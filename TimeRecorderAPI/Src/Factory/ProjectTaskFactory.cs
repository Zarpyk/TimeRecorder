using TimeRecorderAPI.DB;
using TimeRecorderAPI.DTO;
using TimeRecorderDomain.Models;

namespace TimeRecorderAPI.Factory {
    public class ProjectTaskFactory(IDataBaseManager dataBaseManager) {
        public async Task<ProjectTask?> CreateTask(ProjectTaskDTO? projectTaskDTO) {
            if (projectTaskDTO == null) return null;
            ProjectTask projectTaskController = new() {
                Name = projectTaskDTO.Name,
                TimeEstimated = projectTaskDTO.TimeEstimated,
                TimeRecords = projectTaskDTO.TimeRecords
            };

            if (projectTaskDTO.ProjectID != null)
                projectTaskController.Project = await dataBaseManager.Find<Project>(projectTaskDTO.ProjectID.ToString()!);

            if (projectTaskDTO.TagIDs != null) {
                projectTaskController.Tags = [];
                foreach (Guid tagID in projectTaskDTO.TagIDs) {
                    Tag? tag = await dataBaseManager.Find<Tag>(tagID.ToString());
                    if (tag == null) continue;
                    projectTaskController.Tags.Add(tag);
                }
            }
            return projectTaskController;
        }

        public ProjectTaskDTO? CreateTaskDTO(ProjectTask? projectTask) {
            if (projectTask == null) return null;
            ProjectTaskDTO projectTaskDTO = new() {
                ID = new Guid(projectTask.ID),
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