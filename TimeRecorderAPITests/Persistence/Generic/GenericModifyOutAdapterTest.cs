using FluentAssertions;
using TimeRecorderAPI.Adapter.Out.Persistence.GenericAdapters;
using TimeRecorderAPITests.Fixtures;
using Xunit;

namespace TimeRecorderAPITests.Persistence.Generic {
    public class GenericModifyOutAdapterTest : IClassFixture<TestDTOFixture> {
        private readonly TestDTOFixture _dto;

        private readonly GenericModifyOutAdapter<TestEntity, TestDTO, TestFactory> _adapter;

        public GenericModifyOutAdapterTest(TestDTOFixture dto) {
            TestDataBaseFixture dataBase = new(dto);
            _dto = dto;

            TestFactory factory = new();
            _adapter = new GenericModifyOutAdapter<TestEntity, TestDTO, TestFactory>(dataBase.Get(), factory);
        }

        [Fact(DisplayName = "Given a existing ID and ObjectDTO, " +
                            "When replace Object, " +
                            "Then the modified ObjectDTO is returned")]
        public async Task ReplaceExistingObject() {
            TestDTO newDTO = new() {
                ID = null,
                SomeProperty = "New Property"
            };

            TestDTO? findTask = await _adapter.Replace(_dto.ID, newDTO);

            findTask.Should().BeEquivalentTo(newDTO, options => options.Excluding(x => x.ID))
                    .And.Match<TestDTO>(x => x.ID != null && x.ID.ToString()! == _dto.ID);
        }

        [Fact(DisplayName = "Given a non-existing ID and ObjectDTO, " +
                            "When replace Object, " +
                            "Then null is returned")]
        public async Task ReplaceNonExistingObject() {
            TestDTO? findTask = await _adapter.Replace("non-existing-id", _dto.Get());

            findTask.Should().BeNull();
        }
    }
}