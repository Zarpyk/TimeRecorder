using System.Text.Json.Serialization;

namespace TimeRecorderAPI.Exceptions.Responses {
    public class ValidationExceptionResponse : GlobalExceptionResponse {
        [JsonPropertyOrder(1)]
        public required IDictionary<string, string[]> Errors { get; set; }
    }
}