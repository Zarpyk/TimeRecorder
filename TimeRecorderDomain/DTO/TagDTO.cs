namespace TimeRecorderDomain.DTO {
    public record TagDTO : IDTO {
        public Guid? ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = "#FFFFFF";
    }
}