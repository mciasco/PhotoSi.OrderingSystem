using Microsoft.AspNetCore.Mvc;

namespace Users.WebApi.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(
            RequestDelegate next,
            ILogger<ErrorHandlingMiddleware> logger)
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
            catch (ArgumentOutOfRangeException exception)
            {
                _logger.LogError(
                    exception, "Exception occurred: {Message}", exception.Message);

                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Client Error",
                    Detail = exception.Message
                };

                context.Response.StatusCode =
                    StatusCodes.Status404NotFound;

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
            catch (ArgumentException exception)
            {
                _logger.LogError(
                    exception, "Exception occurred: {Message}", exception.Message);

                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Client Error",
                    Detail = exception.Message
                };

                context.Response.StatusCode =
                    StatusCodes.Status400BadRequest;

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
            catch (Exception exception)
            {
                _logger.LogError(
                    exception, "Exception occurred: {Message}", exception.Message);

                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Server Error",
                    Detail = exception.Message
                };

                context.Response.StatusCode =
                    StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
