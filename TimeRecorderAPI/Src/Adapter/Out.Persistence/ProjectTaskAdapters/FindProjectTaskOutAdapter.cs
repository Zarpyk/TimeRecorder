using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderAPI.Configuration.Adapter;
using TimeRecorderAPI.DB;
using TimeRecorderAPI.DTO;
using TimeRecorderDomain.Models;

namespace TimeRecorderAPI.Adapter.Out.Persistence.ProjectTaskAdapters {
    [PortAdapter(typeof(IFindProjectTaskOutPort))]
    public class FindProjectTaskOutAdapter(IDataBaseManager db) : IFindProjectTaskOutPort {
        public async Task<ProjectTaskDTO?> FindTask(string id) {
            throw new NotImplementedException();
        }
    }
}