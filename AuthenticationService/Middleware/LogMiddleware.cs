using AuthenticationService.BLL.Services;

namespace AuthenticationService.BLL.Middleware
{
    public class LogMiddleware
    {
        private readonly Services.ILogger _logger;
        private readonly RequestDelegate _next;

        public LogMiddleware(RequestDelegate next, Services.ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string ip = httpContext.Connection.RemoteIpAddress.ToString();
            _logger.WriteEvent("Я твой Middleware, IP адрес - " + ip);
            await _next(httpContext);
        }
    }
}
