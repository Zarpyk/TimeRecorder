using FluentAssertions;
using Moq;
using TimeRecorderAPI.Application.Port.Out.Persistence.GenericPort;
using TimeRecorderAPI.Application.Service.GenericService;
using TimeRecorderAPITests.Fixtures;
using Xunit;

namespace TimeRecorderAPITests.Service.Generic {
    public class GenericFindServiceTest : IClassFixture<TestDTOFixture> {
        private readonly TestDTOFixture _dto;

        private readonly Mock<IGenericFindOutPort<TestDTO>> _outPort;
        private readonly GenericFindService<TestDTO> _service;

        public GenericFindServiceTest(TestDTOFixture dto) {
            _dto = dto;
            _outPort = new Mock<IGenericFindOutPort<TestDTO>>();

            SetupPorts();

            _service = new GenericFindService<TestDTO>(_outPort.Object);
        }

        private void SetupPorts() {
            _outPort.Setup(x => x.Find(It.IsAny<string>()))
                                   .ReturnsAsync((TestDTO?) null);
            _outPort.Setup(x => x.Find(_dto.ID))
                                   .ReturnsAsync(_dto.Get());
        }

        [Fact(DisplayName = "Given a existing task ID, " +
                            "When FindTask is called, " +
                            "Then the task is returned.")]
        public async Task FindObjectWithExistingID() {
            TestDTO? dto = await _service.Find(_dto.ID);
            dto.Should().BeEquivalentTo(_dto.Get());
        }

        [Fact(DisplayName = "Given a non-existing task ID, " +
                            "When FindTask is called, " +
                            "Then null is returned.")]
        public async Task FindObjectWithNonExistingID() {
            TestDTO? dto = await _service.Find("non-existing-id");
            dto.Should().BeNull();
        }
    }
}