using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderAPI.Configuration.Adapter;
using TimeRecorderAPI.DB;
using TimeRecorderAPI.Factory;
using TimeRecorderAPI.Models;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Adapter.Out.Persistence.ProjectTaskAdapters {
    [PortAdapter(typeof(IFindProjectTaskOutPort))]
    public class FindProjectTaskOutAdapter(
        IDataBaseManager db,
        ProjectTaskFactory factory
    ) : IFindProjectTaskOutPort {
        public async Task<ProjectTaskDTO?> FindTask(string id) {
            ProjectTask? projectTask = await db.Find<ProjectTask>(id);
            
            return factory.CreateTaskDTO(projectTask);
        }
    }
}