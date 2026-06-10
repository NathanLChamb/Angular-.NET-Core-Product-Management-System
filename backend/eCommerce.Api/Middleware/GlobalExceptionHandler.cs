using eCommerce.Application.Exceptions;
using System.Net;

namespace eCommerce.Api.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IHostEnvironment _env;
        private readonly RequestDelegate _next;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IHostEnvironment env, RequestDelegate next)
        {
            _logger = logger;
            _env = env;
            _next = next;
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
                    ValidationException => HttpStatusCode.BadRequest,
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
