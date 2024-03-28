namespace TimeRecorderDomain.DTO {
    public record ProjectDTO(Guid? ID) {
        public Guid? ID { get; internal set; } = ID;
        public string? Name { get; set; }
        public string? Color { get; set; }

    }
}