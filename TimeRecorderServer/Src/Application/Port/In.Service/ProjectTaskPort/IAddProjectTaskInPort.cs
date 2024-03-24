using TimeRecorderServer.DTO;

namespace TimeRecorderServer.Application.Port.In.Service.ProjectTaskPort {
    public interface IAddProjectTaskInPort {
        public Task<bool> AddTask(ProjectTaskDTO projectTaskDTO);
    }
}