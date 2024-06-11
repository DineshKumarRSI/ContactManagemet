using Newtonsoft.Json;
using System.Net;

namespace ContactApplication.API.Helper
{
    public class ExceptionMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorResonse = new ErrorResponse();
            context.Response.ContentType = "application/json";
            errorResonse.StatusCode = HttpStatusCode.InternalServerError;
            errorResonse.Title = "Something wrong.";

            errorResonse.Error = new List<string>();
            errorResonse.Error.Add(exception.Message);

            var result = JsonConvert.SerializeObject(errorResonse);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; ;

            return context.Response.WriteAsync(result);
        }
    }
}
