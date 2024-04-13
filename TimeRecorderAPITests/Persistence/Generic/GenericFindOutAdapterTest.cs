using FluentAssertions;
using TimeRecorderAPI.Adapter.Out.Persistence.GenericAdapters;
using TimeRecorderAPITests.Fixtures;
using Xunit;

namespace TimeRecorderAPITests.Persistence.Generic {
    public class GenericFindOutAdapterTest : IClassFixture<TestDTOFixture> {
        private readonly TestDTOFixture _dto;

        private readonly GenericFindOutAdapter<TestEntity, TestDTO, TestFactory> _adapter;

        public GenericFindOutAdapterTest(TestDTOFixture dto) {
            TestDataBaseFixture dataBase = new(dto);
            _dto = dto;

            TestFactory factory = new();
            _adapter = new GenericFindOutAdapter<TestEntity, TestDTO, TestFactory>(dataBase.Get(), factory);
        }

        [Fact(DisplayName = "Given a existing ID, " +
                            "When find Object, " +
                            "Then the found ObjectDTO is returned.")]
        public async Task FindExistingObject() {
            TestDTO? findTask = await _adapter.Find(_dto.ID);

            findTask.Should().BeEquivalentTo(_dto.Get());
        }

        [Fact(DisplayName = "Given a non-existing ID, " +
                            "When find Object, " +
                            "Then null is returned.")]
        public async Task FindNonExistingObjects() {
            TestDTO? findTask = await _adapter.Find("non-existing-id");

            findTask.Should().BeNull();
        }

        [Fact(DisplayName = "Given nothing, " +
                            "When find all, " +
                            "Then list of ObjectDTO is returned.")]
        public async Task FindAllObjects() {
            List<TestDTO> projectTasks = await _adapter.FindAll();

            projectTasks[0].Should().BeEquivalentTo(_dto.Get());
            projectTasks[1].Should().BeEquivalentTo(_dto.Get2());
        }
    }
}