using TimeRecorderAPI.Application.Port.In.Service.GenericPort;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Port.In.Service.ProjectPort {
    public interface IDeleteProjectInPort : IGenericDeleteInPort<ProjectDTO>;
}