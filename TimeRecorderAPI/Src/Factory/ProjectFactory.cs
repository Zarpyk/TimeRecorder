using TimeRecorderAPI.Configuration.Factory;
using TimeRecorderAPI.Models;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Factory {
    [Factory]
    public class ProjectFactory {
        public Project? CreateProject(ProjectDTO? projectDTO) {
            if (projectDTO == null) return null;

            Project project = new() {
                Name = projectDTO.Name,
                Color = projectDTO.Color
            };

            return project;
        }

        public ProjectDTO? CreateProjectDTO(Project? project) {
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