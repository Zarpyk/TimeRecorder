using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderAPI.DB;
using TimeRecorderAPI.Factory;
using TimeRecorderDomain.Models;

namespace TimeRecorderAPI.Adapter.Out.Persistence.ProjectTaskAdapters {
    public class DeleteProjectTaskOutAdapter(
        IDataBaseManager db,
        ProjectTaskFactory factory
    ) : IDeleteProjectTaskOutPort {

        public async Task<bool> DeleteTask(string id) {
            return await db.Delete<ProjectTask>(id);
        }
    }
}