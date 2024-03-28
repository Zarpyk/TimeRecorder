namespace TimeRecorderDomain.Shared {
    public record TimeRecord(DateTime StartTime, DateTime EndTime) {
        public DateTime StartTime { get; set; } = StartTime;
        public DateTime EndTime { get; set; } = EndTime;
    }
}