using TimeRecorderAPI.Application.Port.Out.Persistence.GenericPort;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Port.Out.Persistence.ProjectTaskPort {
    public interface IFindProjectTaskOutPort : IGenericFindOutPort<ProjectTaskDTO>;
}