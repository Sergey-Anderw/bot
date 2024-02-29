using Master.Domain.Exceptions;
using Shared.Exceptions;
using Shared.Interfaces.Mapping;
using Shared.Validation;

namespace Shared.AutoMapper
{
    public class ExceptionMapper : IExceptionMapper
    {
        public ExceptionInfo Map(Exception exception)
        {
            Assume.NotNull(exception, nameof(exception));
            ExceptionInfo exceptionInfo = exception switch
            {
                DomainException domainException => MapException(domainException),
                BusinessException businessException => MapException(businessException),
                _ => MapException(exception)
            };

            exceptionInfo.ExceptionType = exception.GetType().FullName!;

            return exceptionInfo;
        }

        private static ExceptionInfo MapException(DomainException exception)
        {
            switch (exception)
            {
                case ValidationException validationException:
                    return MapException(validationException);

                default:
                    {
                        var code = CodeFromDomainException(exception);
                        var message = MessageFromDomainException(exception);

                        return new ExceptionInfo
                        {
                            Code = code,
                            Message = message,
                            StackTrace = exception.StackTrace!,
                            InnerMessage = exception.InnerException?.Message!,
                            Data = exception.Data
                        };
                    }
            }
        }
        private static int CodeFromDomainException(DomainException exception)
        {
            return exception switch
            {
                _ => ResponseCodes.Unknown
            };
        }

        private static ExceptionInfo MapException(BusinessException exception)
        {
            return new ExceptionInfo
            {
                Code = exception.Code,
                Message = exception.Message,
                StackTrace = exception.StackTrace!,
                InnerMessage = exception.InnerException?.Message!,
                Data = exception.Data
            };
        }

        private static ExceptionInfo MapException(Exception exception)
        {
            return new ExceptionInfo
            {
                Code = ResponseCodes.Unknown,
                Message = exception.Message,
                StackTrace = exception.StackTrace!,
                InnerMessage = InnerMessage(exception.InnerException!),
                Data = exception.Data
            };
        }

        private static string MessageFromDomainException(DomainException exception)
        {
            return exception switch
            {
                _ => exception.Message
            };
        }

        private static string InnerMessage(Exception? exception)
        {
            if (exception == null)
                return null!;

            string innerMessage = null!;
            if (exception.InnerException != null)
                innerMessage = InnerMessage(exception.InnerException);

            return $"{exception.InnerException}\n{innerMessage}";
        }

        private static ExceptionInfo MapException(ValidationException exception)
        {
            string failure = null!;
            if (exception.Failures.Count > 0)
            {
                var kv = exception.Failures.First();
                failure = kv.Value.FirstOrDefault()!;
            }

            return new ExceptionInfo
            {
                Code = ResponseCodes.ValidationFailed,
                Message = failure,
                StackTrace = exception.StackTrace!,
                InnerMessage = exception.InnerException?.Message!,
                Data = exception.Data
            };
        }
    }
}
