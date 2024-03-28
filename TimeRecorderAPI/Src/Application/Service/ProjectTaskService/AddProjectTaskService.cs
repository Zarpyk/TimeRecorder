using TimeRecorderAPI.Application.Port.In.Service.ProjectTaskPort;
using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderAPI.Configuration.Adapter;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Service.ProjectTaskService {
    [PortAdapter(typeof(IAddProjectTaskInPort))]
    public class AddProjectTaskService(IAddProjectTaskOutPort outPort) : IAddProjectTaskInPort {
        public async Task<ProjectTaskDTO> AddTask(ProjectTaskDTO projectTaskDTO) {
            return await outPort.AddTask(projectTaskDTO);
        }
    }
}