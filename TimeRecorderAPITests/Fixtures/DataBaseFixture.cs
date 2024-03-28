using Moq;
using TimeRecorderAPI.DB;
using TimeRecorderAPI.Models;

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
                    ID = projectTaskDTO.ProjectDTOFixture.ID,
                    Name = projectTaskDTO.ProjectDTOFixture.Get().Name,
                    Color = projectTaskDTO.ProjectDTOFixture.Get().Color
                },
                Tags = new HashSet<Tag> {
                    new() {
                        ID = projectTaskDTO.TagDTOFixture.ID,
                        Name = projectTaskDTO.TagDTOFixture.Get().Name,
                        Color = projectTaskDTO.TagDTOFixture.Get().Color
                    }
                }
            };

            #region Find
            _dataBaseManager.Setup(x => x.Find<ProjectTask>(It.IsNotIn(projectTaskDTO.ID)))
                            .ReturnsAsync((ProjectTask?) null);
            _dataBaseManager.Setup(x => x.Find<ProjectTask>(projectTaskDTO.ID))
                            .ReturnsAsync(projectTask);

            _dataBaseManager.Setup(x => x.Find<Project>(It.IsNotIn(projectTaskDTO.ProjectDTOFixture.ID)))
                            .ReturnsAsync((Project?) null);
            _dataBaseManager.Setup(x => x.Find<Project>(projectTaskDTO.ProjectDTOFixture.ID))
                            .ReturnsAsync(projectTask.Project);

            _dataBaseManager.Setup(x => x.Find<Tag>(It.IsNotIn(projectTaskDTO.TagDTOFixture.ID)))
                            .ReturnsAsync((Tag?) null);
            _dataBaseManager.Setup(x => x.Find<Tag>(projectTaskDTO.TagDTOFixture.ID))
                            .ReturnsAsync(projectTask.Tags.First());
            #endregion

            #region Replace
            _dataBaseManager.Setup(x => x.Replace(It.IsAny<ProjectTask>()))
                            .ReturnsAsync((ProjectTask task) => task.ID == projectTaskDTO.ID);
            #endregion

            #region Delete
            _dataBaseManager.Setup(x => x.Delete<ProjectTask>(It.IsAny<string>()))
                            .ReturnsAsync((string id) => id == projectTaskDTO.ID);
            #endregion
        }

        public IDataBaseManager Get() {
            return _dataBaseManager.Object;
        }
    }
}