using TimeRecorderAPI.Application.Port.In.Service.ProjectTaskPort;
using TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort;

namespace TimeRecorderAPI.Application.Service.ProjectTaskService {
    public class DeleteProjectTaskService(IDeleteProjectTaskOutPort outPort) : IDeleteProjectTaskInPort {
        public Task<bool> DeleteTask(string id) {
            return outPort.DeleteTask(id);
        }
    }
}