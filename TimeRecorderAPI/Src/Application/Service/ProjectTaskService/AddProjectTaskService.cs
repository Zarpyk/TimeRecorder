using TimeRecorderAPI.Application.Port.In.Service.ProjectTaskPort;
using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderAPI.Configuration.Adapter;
using TimeRecorderAPI.DTO;
using TimeRecorderAPI.Factory;
using TimeRecorderDomain.Models;

namespace TimeRecorderAPI.Application.Service.ProjectTaskService {
    [PortAdapter(typeof(IAddProjectTaskInPort))]
    public class AddProjectTaskService(
        IAddProjectTaskOutPort outPort,
        ProjectTaskFactory taskFactory
    ) : IAddProjectTaskInPort {

        public async Task<bool> AddTask(ProjectTaskDTO projectTaskDTO) {
            ProjectTask? projectTask = await taskFactory.CreateTask(projectTaskDTO);
            return projectTask != null && await outPort.AddTask(projectTask);
        }
    }
}