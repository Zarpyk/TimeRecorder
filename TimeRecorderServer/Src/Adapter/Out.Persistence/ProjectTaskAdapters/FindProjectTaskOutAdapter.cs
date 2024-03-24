using TimeRecorderDomain.Models;
using TimeRecorderServer.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderServer.Configuration.Adapter;
using TimeRecorderServer.DB;

namespace TimeRecorderServer.Adapter.Out.Persistence.ProjectTaskAdapters {
    [PortAdapter(typeof(IFindProjectTaskOutPort))]
    public class FindProjectTaskOutAdapter(IDataBaseManager db) : IFindProjectTaskOutPort {
        public async Task<ProjectTask?> FindTask(string id) {
            ProjectTask? projectTask = await db.Find<ProjectTask>(id);
            return projectTask;
        }
    }
}