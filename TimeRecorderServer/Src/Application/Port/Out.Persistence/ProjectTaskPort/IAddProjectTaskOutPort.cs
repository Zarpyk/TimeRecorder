using TimeRecorderDomain.Models;

namespace TimeRecorderServer.Application.Port.Out.Persistence.ProjectTaskPort {
    public interface IAddProjectTaskOutPort {
        public Task<bool> AddTask(ProjectTask projectTask);
    }
}