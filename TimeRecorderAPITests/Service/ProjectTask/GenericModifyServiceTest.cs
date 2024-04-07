using FluentAssertions;
using Moq;
using TimeRecorderAPI.Application.Port.Out.Persistence.GenericPort;
using TimeRecorderAPI.Application.Service.GenericService;
using TimeRecorderAPITests.Fixtures;
using Xunit;

namespace TimeRecorderAPITests.Service.ProjectTask {
    public class GenericModifyServiceTest : IClassFixture<TestDTOFixture> {
        private readonly TestDTOFixture _dto;

        private readonly Mock<IGenericModifyOutPort<TestDTO>> _outPort;
        private readonly GenericModifyService<TestDTO> _service;

        public GenericModifyServiceTest(TestDTOFixture dto) {
            _dto = dto;
            _outPort = new Mock<IGenericModifyOutPort<TestDTO>>();

            SetupPorts();

            _service = new GenericModifyService<TestDTO>(_outPort.Object);
        }

        private void SetupPorts() {
            _outPort.Setup(x => x.Replace(_dto.ID, It.IsAny<TestDTO>()))
                    .ReturnsAsync((string id, TestDTO projectTaskDTO) => {
                         projectTaskDTO.ID = new Guid(id);
                         return projectTaskDTO;
                     });
        }

        [Fact(DisplayName = "Given a existing ID, " +
                            "When replace Object , " +
                            "Then the modified ObjectDTO is returned.")]
        public async Task ReplaceExistingTask() {
            TestDTO newDTO = new() {
                ID = null,
                SomeProperty = "New Name"
            };

            TestDTO? dto = await _service.Replace(_dto.ID, newDTO);
            newDTO.ID = new Guid(_dto.ID);
            dto.Should().BeEquivalentTo(newDTO, options => options.Excluding(x => x.ID))
               .And.Match<TestDTO>(x => x.ID != null && x.ID.ToString()! == _dto.ID);
        }

        [Fact(DisplayName = "Given a non-existing ID, " +
                            "When replace Object , " +
                            "Then null is returned.")]
        public async Task ReplaceNonExistingTask() {
            TestDTO? dto = await _service.Replace("non-existing-id", _dto.Get());

            dto.Should().BeNull();
        }
    }
}