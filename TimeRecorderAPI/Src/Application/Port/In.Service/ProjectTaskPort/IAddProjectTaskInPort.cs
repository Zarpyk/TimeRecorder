using TimeRecorderAPI.DTO;

namespace TimeRecorderAPI.Application.Port.In.Service.ProjectTaskPort {
    public interface IAddProjectTaskInPort {
        public Task<bool> AddTask(ProjectTaskDTO projectTaskDTO);
    }
}