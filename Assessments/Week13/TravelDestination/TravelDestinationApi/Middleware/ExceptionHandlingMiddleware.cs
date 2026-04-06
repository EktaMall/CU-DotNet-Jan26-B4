    using System.Net;
    using TravelDestinationApi.Exceptions;

namespace TravelDestinationApi.Middleware
{
    
        public class ExceptionHandlingMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly ILogger<ExceptionHandlingMiddleware> _logger;

            public ExceptionHandlingMiddleware(
                RequestDelegate next,
                ILogger<ExceptionHandlingMiddleware> logger)
            {
                _next = next;
                _logger = logger;
            }

            public async Task InvokeAsync(HttpContext context)
            {
                try
                {
                    await _next(context);
                }
                catch (DestinationNotFoundException ex)
                {
                    _logger.LogWarning(ex, "Destination not found.");
                    await HandleExceptionAsync(context, HttpStatusCode.NotFound, ex.Message);
                }
                catch (ArgumentException ex)
                {
                    _logger.LogWarning(ex, "Validation error.");
                    await HandleExceptionAsync(context, HttpStatusCode.BadRequest, ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unhandled exception.");
                    await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "An unexpected error occurred.");
                }
            }

            private static async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)statusCode;

                var response = new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = message
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    
}
