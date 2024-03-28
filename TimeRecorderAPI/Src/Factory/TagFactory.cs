using TimeRecorderAPI.Configuration.Factory;
using TimeRecorderAPI.Models;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Factory {
    [Factory]
    public class TagFactory {
        public Tag? CreateTag(TagDTO? tagDTO) {
            if (tagDTO == null) return null;

            Tag tag = new() {
                Name = tagDTO.Name,
                Color = tagDTO.Color
            };

            return tag;
        }

        public TagDTO? CreateTagDTO(Tag? tag) {
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