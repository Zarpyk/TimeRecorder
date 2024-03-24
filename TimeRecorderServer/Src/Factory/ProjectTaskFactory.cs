using TimeRecorderDomain.Models;
using TimeRecorderServer.DB;
using TimeRecorderServer.DTO;

namespace TimeRecorderServer.Factory {
    public class ProjectTaskFactory(IDataBaseManager dataBaseManager) {
        public async Task<ProjectTask?> CreateTask(ProjectTaskDTO? projectTaskDTO) {
            if (projectTaskDTO == null) return null;
            ProjectTask projectTask = new() {
                Name = projectTaskDTO.Name,
                TimeEstimated = projectTaskDTO.TimeEstimated,
                TimeRecords = projectTaskDTO.TimeRecords
            };

            if (projectTaskDTO.ProjectID != null)
                projectTask.Project = await dataBaseManager.Find<Project>(projectTaskDTO.ProjectID.ToString()!);

            if (projectTaskDTO.TagIDs != null) {
                projectTask.Tags = [];
                foreach (Guid tagID in projectTaskDTO.TagIDs) {
                    Tag? tag = await dataBaseManager.Find<Tag>(tagID.ToString());
                    if (tag == null) continue;
                    projectTask.Tags.Add(tag);
                }
            }
            return projectTask;
        }

        public ProjectTaskDTO? CreateTaskDTO(ProjectTask? projectTask) {
            if (projectTask == null) return null;
            ProjectTaskDTO projectTaskDTO = new() {
                Id = new Guid(projectTask.ID),
                Name = projectTask.Name,
                TimeEstimated = projectTask.TimeEstimated,
                TimeRecords = projectTask.TimeRecords,
                ProjectID = projectTask.Project == null ? null : new Guid(projectTask.Project.ID),
                TagIDs = projectTask.Tags?.Select(tag => new Guid(tag.ID)).ToHashSet()
            };
            return projectTaskDTO;
        }
    }
}