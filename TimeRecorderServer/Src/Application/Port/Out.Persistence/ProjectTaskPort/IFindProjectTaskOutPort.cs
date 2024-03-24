using TimeRecorderDomain.Models;

namespace TimeRecorderServer.Application.Port.Out.Persistence.ProjectTaskPort {
    public interface IFindProjectTaskOutPort {
        public ProjectTask? FindTask(string id);
    }
}