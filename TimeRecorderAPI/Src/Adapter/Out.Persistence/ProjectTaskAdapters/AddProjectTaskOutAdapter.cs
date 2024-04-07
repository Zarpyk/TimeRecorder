using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderAPI.Configuration.Adapter;
using TimeRecorderAPI.DB;
using TimeRecorderAPI.Factory;
using TimeRecorderAPI.Models;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Adapter.Out.Persistence.ProjectTaskAdapters {
    [PortAdapter(typeof(IAddProjectTaskOutPort))]
    public class AddProjectTaskOutAdapter(
        IDataBaseManager db,
        ProjectTaskFactory factory
    ) : IAddProjectTaskOutPort {
        public async Task<ProjectTaskDTO> Add(ProjectTaskDTO projectTaskDTO) {
            ProjectTask projectTask = (await factory.CreateTask(projectTaskDTO))!;
            await db.Insert(projectTask);
            return factory.CreateTaskDTO(projectTask)!;
        }
    }
}