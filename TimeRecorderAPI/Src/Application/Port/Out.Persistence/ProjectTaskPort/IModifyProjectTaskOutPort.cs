using TimeRecorderAPI.DTO;

namespace TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort {
    public interface IModifyProjectTaskOutPort {
        public Task<ProjectTaskDTO?> ModifyTask(string id, ProjectTaskDTO projectTaskDTO);
    }
}