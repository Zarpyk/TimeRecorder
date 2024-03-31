using Swashbuckle.AspNetCore.Annotations;

namespace TimeRecorderDomain.DTO {
    public record ProjectDTO : IDTO {
        [SwaggerSchema(ReadOnly = true)]
        public Guid? ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = "#FFFFFF";
    }
}