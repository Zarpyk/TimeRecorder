using TimeRecorderDomain.Models;
using TimeRecorderServer.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderServer.Configuration.Adapter;
using TimeRecorderServer.DB;

namespace TimeRecorderServer.Adapter.Out.Persistence.ProjectTaskAdapters {
    [PortAdapter(typeof(IAddProjectTaskOutPort))]
    public class AddProjectTaskOutAdapter(IDataBaseManager db) : IAddProjectTaskOutPort {
        public async Task<bool> AddTask(ProjectTask projectTask) {
            await db.Insert(projectTask);
            return true;
        }
    }
}