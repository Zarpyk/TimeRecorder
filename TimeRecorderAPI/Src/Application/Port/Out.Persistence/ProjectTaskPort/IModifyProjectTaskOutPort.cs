using TimeRecorderAPI.DTO;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort {
    public interface IModifyProjectTaskOutPort {
        public Task<ProjectTaskDTO?> ReplaceTask(string id, ProjectTaskDTO projectTaskDTO);
    }
}