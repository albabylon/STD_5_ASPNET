using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using WebApiAuthenticationService.PLL.Logging;

namespace WebApiAuthenticationService.PLL.Middlewares
{
    public class LogMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public LogMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _logger.WriteEvent($"Я твой Middleware. IP-клиента: {httpContext.Connection.RemoteIpAddress}");
            await _next(httpContext);
        }
    }
}
