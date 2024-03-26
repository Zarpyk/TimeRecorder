using TimeRecorderAPI.Application.Port.In.Service.ProjectTaskPort;
using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderAPI.DTO;

namespace TimeRecorderAPI.Application.Service.ProjectTaskService {
    public class ModifyProjectTaskService(IModifyProjectTaskOutPort outPort) : IModifyProjectTaskInPort {
        public Task<ProjectTaskDTO?> ReplaceTask(string id, ProjectTaskDTO projectTaskDTO) {
            return outPort.ModifyTask(id, projectTaskDTO);
        }
    }
}