using FluentAssertions;
using TimeRecorderAPI.Adapter.Out.Persistence.GenericAdapters;
using TimeRecorderAPITests.Fixtures;
using Xunit;

namespace TimeRecorderAPITests.Persistence.Generic {
    public class GenericDeleteOutAdapterTest : IClassFixture<TestDTOFixture> {
        private readonly TestDTOFixture _dto;

        private readonly GenericDeleteOutAdapter<TestEntity, TestDTO> _adapter;

        public GenericDeleteOutAdapterTest(TestDTOFixture dto) {
            TestDataBaseFixture dataBase = new(dto);
            _dto = dto;

            _adapter = new GenericDeleteOutAdapter<TestEntity, TestDTO>(dataBase.Get());
        }

        [Fact(DisplayName = "Given a existing ID, " +
                            "When delete Object, " +
                            "Then true is returned")]
        public async Task DeleteExistingObject() {
            bool delete = await _adapter.Delete(_dto.ID);

            delete.Should().BeTrue();
        }

        [Fact(DisplayName = "Given a non-existing ID, " +
                            "When delete Object, " +
                            "Then false is returned")]
        public async Task DeleteNonExistingObject() {
            bool delete = await _adapter.Delete("non-existing-id");

            delete.Should().BeFalse();
        }
    }
}