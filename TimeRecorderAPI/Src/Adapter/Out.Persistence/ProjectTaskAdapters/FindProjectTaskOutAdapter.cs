using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderAPI.Configuration.Adapter;
using TimeRecorderAPI.DB;
using TimeRecorderDomain.Models;

namespace TimeRecorderAPI.Adapter.Out.Persistence.ProjectTaskAdapters {
    [PortAdapter(typeof(IFindProjectTaskOutPort))]
    public class FindProjectTaskOutAdapter(IDataBaseManager db) : IFindProjectTaskOutPort {
        public async Task<ProjectTask?> FindTask(string id) {
            ProjectTask? projectTask = await db.Find<ProjectTask>(id);
            return projectTask;
        }
    }
}