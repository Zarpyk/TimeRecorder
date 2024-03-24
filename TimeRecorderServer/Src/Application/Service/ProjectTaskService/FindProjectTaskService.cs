using TimeRecorderDomain.Models;
using TimeRecorderServer.Application.Port.In.Service.ProjectTaskPort;
using TimeRecorderServer.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderServer.Configuration.Adapter;
using TimeRecorderServer.DTO;
using TimeRecorderServer.Factory;

namespace TimeRecorderServer.Application.Service.ProjectTaskService {
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