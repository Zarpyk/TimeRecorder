using TimeRecorderDomain.DTO;

namespace TimeRecorderAPITests.Fixtures {
    public class TagDTOFixture {
        private readonly TagDTO _tagDTO = new() {
            ID = Guid.NewGuid(),
            Name = "Project 1",
            Color = "#FFFFFF"
        };

        public TagDTO Get() {
            return _tagDTO;
        }

        public string ID => _tagDTO.ID.ToString()!;
    }
}