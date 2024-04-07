using TimeRecorderAPI.Application.Port.In.Service.ProjectTaskPort;
using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderAPI.Configuration.Adapter;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Service.ProjectTaskService {
    [PortAdapter(typeof(IAddProjectTaskInPort))]
    public class AddProjectService(IAddProjectTaskOutPort outPort) : IAddProjectTaskInPort {
        public async Task<ProjectTaskDTO> Add(ProjectTaskDTO projectTaskDTO) {
            return await outPort.Add(projectTaskDTO);
        }
    }
}