using FluentAssertions;
using Moq;
using TimeRecorderAPI.Application.Port.Out.Persistence.GenericPort;
using TimeRecorderAPI.Application.Service.GenericService;
using TimeRecorderAPITests.Fixtures;
using Xunit;

namespace TimeRecorderAPITests.Service.Generic {
    public class GenericAddServiceTest : IClassFixture<TestDTOFixture> {
        private readonly TestDTOFixture _dto;

        private readonly Mock<IGenericAddOutPort<TestDTO>> _outPort;
        private readonly GenericAddService<TestDTO> _service;

        public GenericAddServiceTest(TestDTOFixture dto) {
            _dto = dto;
            _outPort = new Mock<IGenericAddOutPort<TestDTO>>();

            SetupPorts();

            _service = new GenericAddService<TestDTO>(_outPort.Object);
        }

        private void SetupPorts() {
            _outPort.Setup(x => x.Add(_dto.Get()))
                                  .ReturnsAsync(_dto.Get());
        }

        [Fact(DisplayName = "Given a ObjectDTO, " +
                            "When Add is called, " +
                            "Then ObjectDTO is returned.")]
        public async Task AddNewObject() {
            TestDTO dto = await _service.Add(_dto.Get());
            dto.Should().BeEquivalentTo(_dto.Get());
        }
    }
}