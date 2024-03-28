using TimeRecorderAPI.Application.Port.In.Service.ProjectTaskPort;
using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;
using TimeRecorderAPI.Configuration.Adapter;
using TimeRecorderAPI.DTO;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Service.ProjectTaskService {
    [PortAdapter(typeof(IFindProjectTaskInPort))]
    public class FindProjectTaskService(IFindProjectTaskOutPort outPort) : IFindProjectTaskInPort {

        public async Task<ProjectTaskDTO?> FindTask(string id) {
            return await outPort.FindTask(id);
        }
    }
}