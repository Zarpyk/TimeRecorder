using FluentAssertions;
using Moq;
using TimeRecorderAPI.Application.Port.Out.Persistence.GenericPort;
using TimeRecorderAPI.Application.Service.GenericService;
using TimeRecorderAPITests.Fixtures;
using Xunit;

namespace TimeRecorderAPITests.Service.ProjectTask {
    public class GenericDeleteServiceTest : IClassFixture<TestDTOFixture> {
        private readonly TestDTOFixture _dto;

        private readonly Mock<IGenericDeleteOutPort<TestDTO>> _outPort;
        private readonly GenericDeleteService<TestDTO> _service;

        public GenericDeleteServiceTest(TestDTOFixture dto) {
            _dto = dto;
            _outPort = new Mock<IGenericDeleteOutPort<TestDTO>>();

            SetupPorts();

            _service = new GenericDeleteService<TestDTO>(_outPort.Object);
        }

        private void SetupPorts() {
            _outPort.Setup(x => x.Delete(It.IsNotIn(_dto.ID)))
                                     .ReturnsAsync(false);
            _outPort.Setup(x => x.Delete(_dto.ID))
                                     .ReturnsAsync(true);
        }

        [Fact(DisplayName = "Given a existing ID, " +
                            "When delete Object , " +
                            "Then true is returned.")]
        public async Task DeleteExistingProjectTask() {
            bool result = await _service.Delete(_dto.ID);
            result.Should().BeTrue();
        }
        
        [Fact(DisplayName = "Given a non-existing ID, " +
                            "When delete Object , " +
                            "Then false is returned.")]
        public async Task DeleteNonExistingProjectTask() {
            bool result = await _service.Delete("non-existing-id");
            result.Should().BeFalse();
        }
    }
}