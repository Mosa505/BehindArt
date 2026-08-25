using System.Net;
using System.Text.Json;
namespace BehindArt.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }

        }
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var (statusCode, message) = ex switch
            {
                KeyNotFoundException => (HttpStatusCode.NotFound, ex.Message),
                ArgumentException => (HttpStatusCode.BadRequest, ex.Message),
                InvalidOperationException ioe when ioe.Message.Contains("relationship") =>
            (HttpStatusCode.BadRequest, "Cannot delete this item because other records depend on it."),
                InvalidOperationException => (HttpStatusCode.BadRequest, ex.Message),
                UnauthorizedAccessException => (HttpStatusCode.Unauthorized, "You are not authorized to perform this action."),
                _ => (HttpStatusCode.InternalServerError, "Something went wrong. Please try again later.")
            };
            if (statusCode == HttpStatusCode.InternalServerError)
                _logger.LogError(ex, "Unhandled exception occurred.");
            else
                _logger.LogWarning(ex, "Handled exception occurred: {Message}", ex.Message);

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var response = new
            {
                statusCode = (int)statusCode,
                message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));


        }


    }
}
