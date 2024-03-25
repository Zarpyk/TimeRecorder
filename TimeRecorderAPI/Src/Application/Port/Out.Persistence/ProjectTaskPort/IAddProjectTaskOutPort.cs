using TimeRecorderDomain.Models;

namespace TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort {
    public interface IAddProjectTaskOutPort {
        public Task<bool> AddTask(ProjectTaskController projectTaskController);
    }
}