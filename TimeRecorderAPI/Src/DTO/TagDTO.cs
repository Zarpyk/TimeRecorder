using FluentValidation;

namespace TimeRecorderAPI.DTO {
    public record TagDTO {
        public Guid? ID { get; internal set; }
        public string? Name { get; set; }
        public string? Color { get; set; }
    }

    public class TagDTOValidator : AbstractValidator<TagDTO> {
        public TagDTOValidator() {
            RuleFor(x => x.Color).Matches("^#([A-Fa-f0-9]{6})$");
        }
    }
}