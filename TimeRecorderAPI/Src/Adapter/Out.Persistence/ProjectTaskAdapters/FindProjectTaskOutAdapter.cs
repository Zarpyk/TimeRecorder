using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderAPI.Configuration.Adapter;
using TimeRecorderAPI.DB;
using TimeRecorderDomain.Models;

namespace TimeRecorderAPI.Adapter.Out.Persistence.ProjectTaskAdapters {
    [PortAdapter(typeof(IFindProjectTaskOutPort))]
    public class FindProjectTaskOutAdapter(IDataBaseManager db) : IFindProjectTaskOutPort {
        public async Task<ProjectTaskController?> FindTask(string id) {
            ProjectTaskController? projectTask = await db.Find<ProjectTaskController>(id);
            return projectTask;
        }
    }
}