using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderAPI.Configuration.Adapter;
using TimeRecorderAPI.DB;
using TimeRecorderAPI.DTO;

namespace TimeRecorderAPI.Adapter.Out.Persistence.ProjectTaskAdapters {
    [PortAdapter(typeof(IAddProjectTaskOutPort))]
    public class AddProjectTaskOutAdapter(IDataBaseManager db) : IAddProjectTaskOutPort {
        public Task<ProjectTaskDTO> AddTask(ProjectTaskDTO projectTaskDTO) {
            throw new NotImplementedException();
        }
    }
}