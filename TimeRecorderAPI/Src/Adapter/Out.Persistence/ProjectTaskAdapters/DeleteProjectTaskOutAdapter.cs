using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderAPI.Configuration.Adapter;
using TimeRecorderAPI.DB;
using TimeRecorderAPI.Models;

namespace TimeRecorderAPI.Adapter.Out.Persistence.ProjectTaskAdapters {
    [PortAdapter(typeof(IDeleteProjectTaskOutPort))]
    public class DeleteProjectTaskOutAdapter(IDataBaseManager db) : IDeleteProjectTaskOutPort {
        public async Task<bool> Delete(string id) {
            return await db.Delete<ProjectTask>(id);
        }
    }
}