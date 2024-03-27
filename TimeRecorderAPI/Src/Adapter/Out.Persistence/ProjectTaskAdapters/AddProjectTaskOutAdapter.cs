using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderAPI.Configuration.Adapter;
using TimeRecorderAPI.DB;
using TimeRecorderAPI.DTO;
using TimeRecorderAPI.Factory;
using TimeRecorderDomain.Models;

namespace TimeRecorderAPI.Adapter.Out.Persistence.ProjectTaskAdapters {
    [PortAdapter(typeof(IAddProjectTaskOutPort))]
    public class AddProjectTaskOutAdapter(IDataBaseManager db, ProjectTaskFactory factory) : IAddProjectTaskOutPort {
        public async Task<ProjectTaskDTO> AddTask(ProjectTaskDTO projectTaskDTO) {
            ProjectTask projectTask = (await factory.CreateTask(projectTaskDTO))!;
            await db.Insert(projectTask);
            return factory.CreateTaskDTO(projectTask)!;
        }
    }
}