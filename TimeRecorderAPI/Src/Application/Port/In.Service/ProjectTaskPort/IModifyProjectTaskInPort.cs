using TimeRecorderAPI.DTO;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Port.In.Service.ProjectTaskPort {
    public interface IModifyProjectTaskInPort {
        public Task<ProjectTaskDTO?> ReplaceTask(string id, ProjectTaskDTO projectTaskDTO);
    }
}