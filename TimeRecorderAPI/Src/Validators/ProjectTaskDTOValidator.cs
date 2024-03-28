using FluentValidation;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Validators {
    public class ProjectTaskDTOValidator : AbstractValidator<ProjectTaskDTO> {
        public ProjectTaskDTOValidator() {
        }
    }
}