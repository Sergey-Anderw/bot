using System.Net;
using Application.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Serilog;

namespace Infrastructure.Middleware
{
	public class IgnoreRouteMiddleware(
        RequestDelegate next,
        ILogger logger,
        AppSettings appSettings)
    {
        public async Task Invoke(HttpContext context)
		{

				try
				{
					var queryString = context.Request.Query;
					queryString.TryGetValue("sk", out StringValues requestAccessKey);
					var accessKey = "";

					if (requestAccessKey == accessKey)
						await next(context);
					else
						await HandleForbiddenAsync(context);

				}
				catch (Exception exceptionObj)
				{
					await HandleExceptionAsync(context, exceptionObj, logger);
				}

		}

		private static Task HandleForbiddenAsync(HttpContext context)
		{
			var code = HttpStatusCode.Forbidden; 

			var result = JsonConvert.SerializeObject(new { StatusCode = (int)code});
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)code;
			return context.Response.WriteAsync(result);

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
