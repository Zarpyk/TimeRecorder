using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using SimpleNetLogger;
using TimeRecorderAPI.Exceptions.Responses;

namespace TimeRecorderAPI.Exceptions {
    public class ValidationExceptionHandler : IExceptionHandler {
        private const string ExceptionType = "ValidationException";
        private const string ResponseTitle = "One or more validation errors occurred";
        private const int StatusCode = (int) HttpStatusCode.BadRequest;

        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception,
                                                    CancellationToken cancellationToken) {
            if (exception is ValidationException validationException) {
                Logger.Error($"Trace ID: {context.TraceIdentifier}\n" +
                             $"{exception}");
                
                var responseObj = new ValidationExceptionResponse {
                    Type = ExceptionType,
                    Title = ResponseTitle,
                    Status = StatusCode,
                    Request = $"{context.Request.Method} {context.Request.Path}",
                    Time = DateTime.UtcNow,
                    TraceID = context.TraceIdentifier,
                    Errors = validationException.Errors
                };
                
                context.Response.StatusCode = StatusCode;
                await context.Response.WriteAsJsonAsync(responseObj, cancellationToken: cancellationToken);
                return await ValueTask.FromResult(true);
            }
            return await ValueTask.FromResult(false);
        }
    }
}