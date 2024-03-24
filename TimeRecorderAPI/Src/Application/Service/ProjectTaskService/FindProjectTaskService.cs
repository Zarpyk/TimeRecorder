using TimeRecorderAPI.Application.Port.In.Service.ProjectTaskPort;
using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderAPI.Configuration.Adapter;
using TimeRecorderAPI.DTO;
using TimeRecorderAPI.Factory;
using TimeRecorderDomain.Models;

namespace TimeRecorderAPI.Application.Service.ProjectTaskService {
    [PortAdapter(typeof(IFindProjectTaskInPort))]
    public class FindProjectTaskService(
        IFindProjectTaskOutPort outPort,
        ProjectTaskFactory taskFactory
    ) : IFindProjectTaskInPort {

        public async Task<ProjectTaskDTO?> FindTask(string id) {
            ProjectTask? projectTaskDTO = await outPort.FindTask(id);
            return taskFactory.CreateTaskDTO(projectTaskDTO);
        }
    }
}