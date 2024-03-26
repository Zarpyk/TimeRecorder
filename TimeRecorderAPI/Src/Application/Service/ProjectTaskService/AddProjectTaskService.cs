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

        public async Task<ProjectTaskDTO?> AddTask(ProjectTaskDTO projectTaskDTO) {
            ProjectTaskController? projectTask = await taskFactory.CreateTask(projectTaskDTO);
            if (projectTask != null && await outPort.AddTask(projectTask)) {
                return taskFactory.CreateTaskDTO(projectTask);
            }
            return null;
        }

        public async Task<ProjectTaskDTO?> ReplaceTask(string id, ProjectTaskDTO projectTaskDTO) {
            return projectTaskDTO;
        }
    }
}