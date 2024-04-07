using TimeRecorderAPI.Application.Port.In.Service.GenericPort;
using TimeRecorderAPI.Application.Port.Out.Persistence.GenericPort;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Service.GenericService {
    public class GenericAddService<T>(IGenericAddOutPort<T> outPort) : IGenericAddInPort<T> where T : IDTO {
        public async Task<T> Add(T dto) {
            return await outPort.Add(dto);
        }
    }
}