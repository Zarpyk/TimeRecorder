using TimeRecorderAPI.Application.Port.Out.Persistence.GenericPort;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Port.Out.Persistence.TagPort {
    public interface IAddTagOutPort : IGenericAddOutPort<TagDTO>;
}