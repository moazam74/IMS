using System.Net;
using System.Text.Json;
using IMS.Domain.Exceptions;

namespace IMS.Presentation.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IWebHostEnvironment env)
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            
            var response = new ApiResponse<object>
            {
                Success = false,
                Message = exception.Message
            };

            switch (exception)
            {
                case NotFoundException notFoundEx:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = notFoundEx.Message;
                    break;
                    
                case ValidationException validationEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = validationEx.Message;
                    response.Errors = validationEx.Errors;
                    break;
                    
                case UnauthorizedAccessException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response.Message = "You are not authorized to perform this action";
                    break;
                    
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Message = _env.IsDevelopment() 
                        ? exception.Message 
                        : "An error occurred while processing your request";
                    break;
            }

            var result = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(result);
        }
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
    }
} 