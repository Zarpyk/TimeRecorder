using TimeRecorderAPI.Configuration.Factory;
using TimeRecorderAPI.DB;
using TimeRecorderAPI.Models;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Factory {
    [Factory]
    public class ProjectTaskFactory(
        IDataBaseManager dataBaseManager,
        ProjectFactory projectFactory,
        TagFactory tagFactory
    ) : IFactory<ProjectTask, ProjectTaskDTO> {
        public async Task<ProjectTask?> Create(ProjectTaskDTO? projectTaskDTO) {
            if (projectTaskDTO == null) return null;
            ProjectTask projectTask = new() {
                Name = projectTaskDTO.Name,
                TimeEstimated = projectTaskDTO.TimeEstimated,
                TimeRecords = projectTaskDTO.TimeRecords
            };

            if (projectTaskDTO.Project != null)
                projectTask.Project = await dataBaseManager.Find<Project>(projectTaskDTO.Project.ID.ToString()!);

            if (projectTaskDTO.Tags != null) {
                projectTask.Tags = [];
                foreach (Guid? tagID in projectTaskDTO.Tags.Select(x => x.ID)) {
                    Tag? tag = await dataBaseManager.Find<Tag>(tagID.ToString()!);
                    if (tag == null) continue;
                    projectTask.Tags.Add(tag);
                }
                if (projectTask.Tags.Count == 0) projectTask.Tags = null;
            }

            return projectTask;
        }

        public ProjectTaskDTO? CreateDTO(ProjectTask? projectTask) {
            if (projectTask == null) return null;
            ProjectTaskDTO projectTaskDTO = new() {
                ID = new Guid(projectTask.ID),
                Name = projectTask.Name,
                TimeEstimated = projectTask.TimeEstimated,
                TimeRecords = projectTask.TimeRecords,
                Project = projectFactory.CreateDTO(projectTask.Project),
                Tags = projectTask.Tags?.Select(tagFactory.CreateDTO).ToHashSet()
            };
            return projectTaskDTO;
        }
    }
}