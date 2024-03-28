using Swashbuckle.AspNetCore.Annotations;

namespace TimeRecorderDomain.DTO {
    public record ProjectDTO() {
        [SwaggerSchema(ReadOnly = true)]
        public Guid? ID { get; set; }
        public string? Name { get; set; }
        public string? Color { get; set; }
    }
}