using Newtonsoft.Json;
using TreeAndJournal.Api.Exceptions;
using TreeAndJournal.Application.Exceptions;

namespace TreeAndJournal.Api.Middleware
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
            catch (Exception exception)
            {
                _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

                var exceptionDetails = GetExceptionDetails(exception);

                context.Response.StatusCode = exceptionDetails.StatusCode;

                await context.Response.WriteAsJsonAsync(
                    new { type = exceptionDetails.Type, id = exceptionDetails.Id, data = exceptionDetails.Data });
            }
        }

        private static ExceptionDetails GetExceptionDetails(Exception exception)
        {
            return exception switch
            {
                SecureException secureException => new ExceptionDetails(
                    StatusCodes.Status500InternalServerError,
                    typeof(SecureException).ToString(),
                    secureException.EventId,
                    JsonConvert.SerializeObject(new { message = secureException.Message })),
                Exception commonException => new ExceptionDetails(
                    StatusCodes.Status500InternalServerError,
                    typeof(Exception).ToString(),
                    byte.MinValue,
                    JsonConvert.SerializeObject(new { message = commonException.Message }))
            };
        }
    }
}
