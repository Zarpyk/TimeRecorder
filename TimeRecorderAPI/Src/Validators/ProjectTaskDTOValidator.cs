using FluentValidation;
using TimeRecorderDomain.DTO;
using TimeRecorderDomain.Models;

namespace TimeRecorderAPI.DTO {
    public class ProjectTaskDTOValidator : AbstractValidator<ProjectTaskDTO> {
        public ProjectTaskDTOValidator() { }
    }
}