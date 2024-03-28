using FluentValidation;
using TimeRecorderDomain.Models;

namespace TimeRecorderAPI.DTO {
    public record ProjectDTO {
        public Guid? ID { get; internal set; }
        public string? Name { get; set; }
        public string? Color { get; set; }
    }

    public class ProjectDTOValidator : AbstractValidator<ProjectDTO> {
        public ProjectDTOValidator() {
            RuleFor(x => x.Color).Matches("^#([A-Fa-f0-9]{6})$");
        }
    }
}