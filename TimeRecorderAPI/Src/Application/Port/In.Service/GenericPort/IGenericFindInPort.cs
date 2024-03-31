using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Application.Port.In.Service.GenericPort {
    public interface IGenericFindInPort<T> where T : IDTO {
        /// <summary>
        /// Return a DTO by its ID
        /// </summary>
        /// <param name="id"> The ID of the object </param>
        /// <returns> T type DTO or null if not found </returns>
        public Task<T?> Find(string id);
    }
}