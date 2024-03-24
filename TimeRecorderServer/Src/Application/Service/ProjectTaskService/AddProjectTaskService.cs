using TimeRecorderDomain.Models;
using TimeRecorderServer.Application.Port.In.Service.ProjectTaskPort;
using TimeRecorderServer.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderServer.Configuration.Adapter;
using TimeRecorderServer.DTO;
using TimeRecorderServer.Factory;

namespace TimeRecorderServer.Application.Service.ProjectTaskService {
    [PortAdapter(typeof(IAddProjectTaskInPort))]
    public class AddProjectTaskService(
        IAddProjectTaskOutPort outPort,
        ProjectTaskFactory taskFactory
    ) : IAddProjectTaskInPort {

        public bool AddTask(ProjectTaskDTO projectTaskDTO) {
            ProjectTask? projectTask = taskFactory.CreateTask(projectTaskDTO);
            return projectTask != null && outPort.AddTask(projectTask);
        }
    }
}