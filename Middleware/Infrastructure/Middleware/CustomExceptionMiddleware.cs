using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;

namespace Infrastructure.Middleware
{
    public class CustomExceptionMiddleware(
        RequestDelegate next,
        ILogger logger)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception exceptionObj)
            {
                await HandleExceptionAsync(context, exceptionObj, logger);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger logger)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            logger.Error(ex.Message);

            var result = JsonConvert.SerializeObject(new { StatusCode = (int)code, ErrorMessage = ex.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
