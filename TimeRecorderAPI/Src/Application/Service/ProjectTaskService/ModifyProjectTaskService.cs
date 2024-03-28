using TimeRecorderAPI.Application.Port.In.Service.ProjectTaskPort;
using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderAPI.Configuration.Adapter;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Service.ProjectTaskService {
    [PortAdapter(typeof(IModifyProjectTaskInPort))]
    public class ModifyProjectTaskService(IModifyProjectTaskOutPort outPort) : IModifyProjectTaskInPort {
        public Task<ProjectTaskDTO?> ReplaceTask(string id, ProjectTaskDTO projectTaskDTO) {
            return outPort.ReplaceTask(id, projectTaskDTO);
        }
    }
}