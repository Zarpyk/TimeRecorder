using TimeRecorderAPI.Application.Port.In.Service.GenericPort;
using TimeRecorderAPI.Application.Port.Out.Persistence.GenericPort;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Service.GenericService {
    public class GenericFindService<T>(IGenericFindOutPort<T> outPort) : IGenericFindInPort<T> where T : IDTO {
        public async Task<T?> Find(string id) {
            return await outPort.Find(id);
        }

        public async Task<List<T>> FindAll() {
            return await outPort.FindAll();
        }
    }
}