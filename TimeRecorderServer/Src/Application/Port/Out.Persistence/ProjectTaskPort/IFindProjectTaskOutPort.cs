using TimeRecorderDomain.Models;

namespace TimeRecorderServer.Application.Port.Out.Persistence.ProjectTaskPort {
    public interface IFindProjectTaskOutPort {
        public Task<ProjectTask?> FindTask(string id);
    }
}