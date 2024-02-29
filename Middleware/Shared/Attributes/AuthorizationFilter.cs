using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Shared.Attributes
{
    public class AuthorizationFilter : IAsyncActionFilter
    {
        private readonly string? _allowedIp;
        public AuthorizationFilter(IConfiguration configuration)
        {
            var allowedIp = configuration.GetValue<string>("AllowedIp");
            if(allowedIp != null)
	            _allowedIp = allowedIp;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!string.IsNullOrEmpty(_allowedIp))
            {
                var allowedIpArray = _allowedIp.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();
                var ip = context.HttpContext.Connection.RemoteIpAddress;
                if (ip != null && allowedIpArray.Contains(ip.ToString()))
                {
                    await next();
                }
            }

            context.Result = new JsonResult(new { HttpStatusCode.Forbidden });
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
        }
    }
}
