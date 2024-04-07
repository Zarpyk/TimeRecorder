using TimeRecorderAPI.Configuration.Factory;
using TimeRecorderAPI.Models;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Factory {
    [Factory]
    public class TagFactory : IFactory<Tag, TagDTO> {
        public async Task<Tag?> Create(TagDTO? tagDTO) {
            if (tagDTO == null) return null;

            Tag tag = new() {
                Name = tagDTO.Name,
                Color = tagDTO.Color
            };

            return await Task.FromResult(tag);
        }

        public TagDTO? CreateDTO(Tag? tag) {
            if (tag == null) return null;

            TagDTO tagDTO = new() {
                ID = new Guid(tag.ID),
                Name = tag.Name,
                Color = tag.Color
            };

            return tagDTO;
        }
    }
}