using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using SimpleNetLogger;
using TimeRecorderAPI.Exceptions.Responses;

namespace TimeRecorderAPI.Exceptions {
    public class GlobalExceptionHandler : IExceptionHandler {
        private const string ResponseTitle = "An unexpected error occurred";
        private const int StatusCode = (int) HttpStatusCode.InternalServerError;
        
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception,
                                                    CancellationToken cancellationToken) {
            Logger.Error($"Trace ID: {context.TraceIdentifier}\n" +
                         $"{exception}");
            
            var responseObj = new GlobalExceptionResponse {
                Type = exception.GetType().Name,
                Title = ResponseTitle,
                Status = StatusCode,
                Request = $"{context.Request.Method} {context.Request.Path}",
                TraceID = context.TraceIdentifier,
                Time = DateTime.UtcNow
            };
            
            await context.Response.WriteAsJsonAsync(responseObj, cancellationToken: cancellationToken);
            return await ValueTask.FromResult(true);
        }
    }
}