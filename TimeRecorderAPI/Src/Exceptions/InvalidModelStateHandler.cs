using System.Net;
using Microsoft.AspNetCore.Mvc;
using TimeRecorderAPI.Exceptions.Responses;

namespace TimeRecorderAPI.Exceptions {
    public static class InvalidModelStateHandler {
        private const string ExceptionType = "ValidationException";
        private const string ResponseTitle = "One or more validation errors occurred";
        private const int StatusCode = (int) HttpStatusCode.BadRequest;

        public static IActionResult Response(ActionContext context) {
            ValidationException exception = new(context.ModelState
                                                       .ToDictionary(x => x.Key,
                                                                     x => x.Value != null ?
                                                                              x.Value.Errors
                                                                               .Select(e => e.ErrorMessage)
                                                                               .ToArray() :
                                                                              Array.Empty<string>()));
            ValidationExceptionResponse response = new() {
                Type = ExceptionType,
                Title = ResponseTitle,
                Status = StatusCode,
                Request = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}",
                Time = DateTime.UtcNow,
                TraceID = context.HttpContext.TraceIdentifier,
                Errors = exception.Errors
            };

            return new BadRequestObjectResult(response);
        }
    }
}