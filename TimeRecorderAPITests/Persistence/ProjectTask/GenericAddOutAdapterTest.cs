using FluentAssertions;
using TimeRecorderAPI.Adapter.Out.Persistence.GenericAdapters;
using TimeRecorderAPITests.Fixtures;
using Xunit;

namespace TimeRecorderAPITests.Persistence.ProjectTask {
    public class GenericAddOutAdapterTest : IClassFixture<TestDTOFixture> {
        private readonly TestDTOFixture _dto;

        private readonly GenericAddOutAdapter<TestEntity, TestDTO, TestFactory> _adapter;

        public GenericAddOutAdapterTest(TestDTOFixture dto) {
            TestDataBaseFixture dataBase = new(dto);
            _dto = dto;

            TestFactory factory = new();
            _adapter = new GenericAddOutAdapter<TestEntity, TestDTO, TestFactory>(dataBase.Get(), factory);
        }

        [Fact(DisplayName = "Given ObjectDTO, " +
                            "When add Object, " +
                            "Then ObjectDTO is returned.")]
        public async Task AddObject() {
            TestDTO findTask = await _adapter.Add(_dto.Get());

            findTask.Should().BeEquivalentTo(_dto.Get(), options => options.Excluding(x => x.ID));
        }
    }
}