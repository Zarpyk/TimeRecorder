using TimeRecorderDomain.DTO;
using TimeRecorderDomain.Shared;

namespace TimeRecorderAPITests.Fixtures {
    public class ProjectDTOFixture {
        private readonly ProjectDTO _projectDTO = new() {
            ID = Guid.NewGuid(),
            Name = "Project 1",
            Color = "#FFFFFF"
        };

        public ProjectDTO Get() {
            return _projectDTO;
        }

        public string ID => _projectDTO.ID.ToString()!;
    }
}