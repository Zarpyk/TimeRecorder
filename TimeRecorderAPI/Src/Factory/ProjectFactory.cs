using TimeRecorderAPI.Configuration.Factory;
using TimeRecorderAPI.Models;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Factory {
    [Factory]
    public class ProjectFactory : IFactory<Project, ProjectDTO> {
        public async Task<Project?> Create(ProjectDTO? projectDTO) {
            if (projectDTO == null) return null;

            Project project = new() {
                Name = projectDTO.Name,
                Color = projectDTO.Color
            };

            return await Task.FromResult(project);
        }

        public ProjectDTO? CreateDTO(Project? project) {
            if (project == null) return null;
            ProjectDTO projectDTO = new() {
                ID = new Guid(project.ID),
                Name = project.Name,
                Color = project.Color
            };
            return projectDTO;
        }
    }
}