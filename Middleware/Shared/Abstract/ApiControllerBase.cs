using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog.Context;
using Serilog.Events;
using Shared.Extensions;
using Shared.Interfaces.Mapping;
using ILogger = Serilog.ILogger;


namespace Shared.Abstract
{
    public class ApiControllerBase : ControllerBase
    {
        private IExceptionMapper? _exceptionMapper;
        private IMediator? _mediator;
        private ILogger? _logger;

        /// <summary>
        /// Mediator
        /// </summary>
        protected IMediator Mediator => _mediator ??= (HttpContext.RequestServices.GetService(typeof(IMediator)) as IMediator)!;

        /// <summary>
        /// ExceptionMapper
        /// </summary>
        protected IExceptionMapper ExceptionMapper => _exceptionMapper ??= (HttpContext.RequestServices.GetService(typeof(IExceptionMapper)) as IExceptionMapper)!;

        /// <summary>
        /// Logger
        /// </summary>
        protected ILogger Log => _logger ??= (HttpContext.RequestServices.GetService(typeof(ILogger)) as ILogger)!;

        protected async Task<IActionResult> GuideActionAsync<TCommand>(TCommand command)
        {
            var messageId = Guid.NewGuid();
            using (LogContext.PushProperty("CorrelationId", messageId))
            {
                try
                {
                    LogIncomingMessage(command!);
                    var responseMessage = await GetResponse(command);
                    LogOutgoingMessage(responseMessage);

                    return CreateActionResult(responseMessage);
                }
                catch (Exception e)
                {
                    LogIncomingMessageExeption(command!, e);
                    return CreateExceptionActionResult(e);
                }
            }
        }

        private async Task<object> GetResponse<TCommand>(TCommand command)
        {
            return (await Mediator.Send(command!, CancellationToken.None))!;

        }

        private IActionResult CreateActionResult<TResponse>(TResponse messageResponse)
        {
            return Ok(messageResponse);
        }

        private IActionResult CreateExceptionActionResult(Exception exception)
        {
            var exceptionInfo = ExceptionMapper.Map(exception);

            var errorResponseMessage = new ErrorResponse
            {
                Info = new ErrorResponse.ResponseInfo
                {
                    CodeMsg = exceptionInfo.Message,
                    StackTrace = exceptionInfo.StackTrace,
                    Inner = exceptionInfo.InnerMessage,
                    Data = exceptionInfo.Data
                }
            };
            LogOutgoingMessage(errorResponseMessage);

            return CreateActionResult(errorResponseMessage);
        }

        private void LogIncomingMessage(object message)
        {
            if (!Log.IsEnabled(LogEventLevel.Information))
                return;

            Log.InformationEvent(
                LogEvent.IncomingMessage,
                "{ReqName} {PathAndQuery} >>> {@Message}",
                message.GetType().GetFriendlyName(),
                Request.RequestUri().PathAndQuery,
                message);
        }

        private void LogOutgoingMessage(object message)
        {
            if (!Log.IsEnabled(LogEventLevel.Information))
                return;

            Log.DebugEvent(
                LogEvent.OutgoingMessage,
                "{RespName} {PathAndQuery} <<< {@Message}",
                message.GetType().GetFriendlyName(),
                Request.RequestUri().PathAndQuery,
                message);
        }

        private void LogIncomingMessageExeption(object message, Exception e)
        {
            if (!Log.IsEnabled(LogEventLevel.Error))
                return;

            Log.ErrorEvent(
                LogEvent.IncomingMessage,
                "{ReqName} {PathAndQuery} >>> {@Message}",
                e,
                message.GetType().GetFriendlyName(),
                Request.RequestUri().PathAndQuery,                
                message);
        }

    }
}
