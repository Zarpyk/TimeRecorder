using TimeRecorderServer.DTO;

namespace TimeRecorderServer.Application.Port.In.Service.ProjectTaskPort {
    public interface IFindProjectTaskInPort {
        public ProjectTaskDTO? FindTask(string id);
    }
}