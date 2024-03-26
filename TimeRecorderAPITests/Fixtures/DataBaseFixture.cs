using Moq;
using TimeRecorderAPI.DB;
using TimeRecorderDomain.Models;

namespace TimeRecorderAPITests.Fixtures {
    public class DataBaseFixture {
        private readonly Mock<IDataBaseManager> _dataBaseManager = new();

        public DataBaseFixture(ProjectTaskDTOFixture projectTaskDTO) {
            ProjectTask projectTask = new() {
                ID = projectTaskDTO.ID,
                Name = projectTaskDTO.Get().Name,
                TimeEstimated = projectTaskDTO.Get().TimeEstimated,
                TimeRecords = projectTaskDTO.Get().TimeRecords,
                Project = new Project {
                    ID = projectTaskDTO.Get().ProjectID.ToString()!
                },
                Tags = new HashSet<Tag> {
                    new() {
                        ID = projectTaskDTO.Get().TagIDs!.First().ToString()!
                    }
                }
            };

            _dataBaseManager.Setup(x => x.Find<ProjectTask>(It.IsNotIn(projectTaskDTO.ID)))
                            .ReturnsAsync((ProjectTask?) null);
            _dataBaseManager.Setup(x => x.Find<ProjectTask>(projectTaskDTO.ID))
                            .ReturnsAsync(projectTask);
        }

        public IDataBaseManager Get() {
            return _dataBaseManager.Object;
        }
    }
}