using TimeRecorderAPI.Application.Port.In.Service.TagPort;
using TimeRecorderAPI.Application.Port.Out.Persistence.TagPort;
using TimeRecorderAPI.Application.Service.GenericService;
using TimeRecorderAPI.Configuration.Adapter;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Service.TagService {
    [PortAdapter(typeof(IFindTagInPort))]
    public class FindTagService(IFindTagOutPort outPort)
        : GenericFindService<TagDTO>(outPort), IFindTagInPort;
}