using Moq;
using TimeRecorderAPI.DB;
using TimeRecorderAPI.Models;

namespace TimeRecorderAPITests.Fixtures {
    public class TestDataBaseFixture {
        private readonly Mock<IDataBaseManager> _dataBaseManager = new();

        public TestDataBaseFixture(TestDTOFixture dto) {
            TestEntity entity = new() {
                ID = dto.ID,
                SomeProperty = dto.Get().SomeProperty
            };
            
            TestEntity entity2 = new() {
                ID = dto.Get2().ID.ToString()!,
                SomeProperty = dto.Get2().SomeProperty
            };

            #region Find
            _dataBaseManager.Setup(x => x.FindAll<TestEntity>())
                            .ReturnsAsync([entity, entity2]);
            
            _dataBaseManager.Setup(x => x.Find<TestEntity>(It.IsNotIn(dto.ID)))
                            .ReturnsAsync((TestEntity?) null);
            _dataBaseManager.Setup(x => x.Find<TestEntity>(dto.ID))
                            .ReturnsAsync(entity);
            #endregion

            #region Replace
            _dataBaseManager.Setup(x => x.Replace(It.IsAny<TestEntity>()))
                            .ReturnsAsync((TestEntity task) => task.ID == dto.ID);
            #endregion

            #region Delete
            _dataBaseManager.Setup(x => x.Delete<TestEntity>(It.IsAny<string>()))
                            .ReturnsAsync((string id) => id == dto.ID);
            #endregion
        }

        public IDataBaseManager Get() {
            return _dataBaseManager.Object;
        }
    }
}