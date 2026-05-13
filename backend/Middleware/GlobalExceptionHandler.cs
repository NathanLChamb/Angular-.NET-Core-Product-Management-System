using eCommercePractice4.Application.Exceptions;
using System.Net;

namespace eCommercePractice4.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IHostEnvironment _env;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");

                HttpStatusCode statusCode = ex switch
                {
                    NotFoundException => HttpStatusCode.NotFound,
                    _ => HttpStatusCode.InternalServerError
                };

                var response = new
                {
                    success = false,
                    message = _env.IsDevelopment() ? ex.Message : "An unexpected error occurred",
                    inner = _env.IsDevelopment() ? ex.InnerException?.Message : null,
                    stack = _env.IsDevelopment() ? ex.StackTrace : null
                };

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)statusCode;

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}