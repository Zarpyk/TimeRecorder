using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderAPI.Configuration.Adapter;
using TimeRecorderAPI.DB;
using TimeRecorderAPI.Factory;
using TimeRecorderDomain.DTO;
using TimeRecorderDomain.Models;

namespace TimeRecorderAPI.Adapter.Out.Persistence.ProjectTaskAdapters {
    [PortAdapter(typeof(IModifyProjectTaskOutPort))]
    public class ModifyProjectTaskOutAdapter(
        IDataBaseManager db,
        ProjectTaskFactory factory
    ) : IModifyProjectTaskOutPort {

        public async Task<ProjectTaskDTO?> ReplaceTask(string id, ProjectTaskDTO projectTaskDTO) {
            ProjectTask? projectTask = await factory.CreateTask(projectTaskDTO);
            projectTask!.ID = id;
            bool replace = await db.Replace(projectTask);
            return !replace ? null : factory.CreateTaskDTO(projectTask);
        }
    }
}