using TimeRecorderServer.DTO;

namespace TimeRecorderServer.Application.Port.In.Service.ProjectTaskPort {
    public interface IFindProjectTaskInPort {
        public Task<ProjectTaskDTO?> FindTask(string id);
    }
}