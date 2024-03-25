namespace TimeRecorderAPI.Exceptions.Responses {
    public class GlobalExceptionResponse {
        public required string Title { get; set; }
        public required string Type { get; set; }
        public required int Status { get; set; }
        public required string Request { get; set; }
        public required DateTime Time { get; set; }
        public required string TraceID { get; set; }
    }
}