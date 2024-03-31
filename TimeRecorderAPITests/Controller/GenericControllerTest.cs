using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TimeRecorderAPI.Adapter.In.RestController;
using TimeRecorderAPI.Application.Port.In.Service.GenericPort;
using TimeRecorderAPITests.Fixtures;
using Xunit;

namespace TimeRecorderAPITests.Controller {
    public class GenericControllerTest : IClassFixture<TestDTOFixture> {
        private readonly TestDTOFixture _dto;

        private readonly Mock<IGenericFindInPort<TestDTO>> _findInPort;
        private readonly Mock<IGenericAddInPort<TestDTO>> _addInPort;
        private readonly Mock<IGenericModifyInPort<TestDTO>> _modifyInPort;
        private readonly Mock<IGenericDeleteInPort<TestDTO>> _deleteInPort;
        private readonly IValidator<TestDTO> _validator = new TestDTOValidator();
        private bool _created;

        private readonly GenericController<TestDTO> _controller;

        public GenericControllerTest(TestDTOFixture dto) {
            _dto = dto;

            _findInPort = new Mock<IGenericFindInPort<TestDTO>>();
            _addInPort = new Mock<IGenericAddInPort<TestDTO>>();
            _modifyInPort = new Mock<IGenericModifyInPort<TestDTO>>();
            _deleteInPort = new Mock<IGenericDeleteInPort<TestDTO>>();

            SetupPorts();


            _controller = new GenericController<TestDTO>(_findInPort.Object,
                                                         _addInPort.Object,
                                                         _modifyInPort.Object,
                                                         _deleteInPort.Object,
                                                         _validator);
        }

        private void SetupPorts() {
            _findInPort.When(() => !_created)
                                  .Setup(x => x.Find(It.IsAny<string>()))
                                  .ReturnsAsync((TestDTO?) null);
            _findInPort.When(() => _created)
                                  .Setup(x => x.Find(_dto.ID))
                                  .ReturnsAsync(_dto.Get);

            _addInPort.Setup(x => x.Add(_dto.Get()))
                                 .ReturnsAsync(_dto.Get())
                                 .Callback(() => _created = true);

            _modifyInPort.When(() => !_created)
                                    .Setup(x => x.Replace(It.IsAny<string>(), It.IsAny<TestDTO>()))
                                    .ReturnsAsync((TestDTO?) null);
            _modifyInPort.When(() => _created)
                                    .Setup(x => x.Replace(_dto.ID, It.IsAny<TestDTO>()))
                                    .ReturnsAsync((string id, TestDTO dto) => {
                                         dto.ID = new Guid(id);
                                         return dto;
                                     });

            _deleteInPort.When(() => !_created)
                                    .Setup(x => x.Delete(It.IsAny<string>()))
                                    .ReturnsAsync(false);
            _deleteInPort.When(() => _created)
                                    .Setup(x => x.Delete(_dto.ID))
                                    .ReturnsAsync(true);
        }

        [Fact(DisplayName = "Given a existing id, " +
                            "When getting a Object, " +
                            "Then Ok ObjectDTO returned.")]
        public async Task GetDTOWithExistingID() {
            await _controller.Post(_dto.Get());
            IActionResult result = await _controller.Get(_dto.ID);

            result.Should().BeOfType<OkObjectResult>()
                  .Which.Value.Should().BeEquivalentTo(_dto.Get());
        }

        [Fact(DisplayName = "Given a non-existing id, " +
                            "When getting a Object, " +
                            "Then NotFound is returned.")]
        public async Task GetDTOWithNonExistingID() {
            IActionResult result = await _controller.Get(_dto.ID);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact(DisplayName = "Given a valid ProjectTaskDTO, " +
                            "When posting a Object, " +
                            "Then Created ObjectDTO is returned.")]
        public async Task PostValidDTO() {
            IActionResult result = await _controller.Post(_dto.Get());

            result.Should().BeOfType<CreatedAtActionResult>()
                  .Which.Value.Should().BeEquivalentTo(_dto.Get());
        }

        [Fact(DisplayName = "Given a existing id and valid ObjectDTO, " +
                            "When putting a Object, " +
                            "Then Ok ObjectDTO is returned.")]
        public async Task PutValidDTO() {
            await _controller.Post(_dto.Get());
            TestDTO newProjectTaskDTO = new() {
                ID = null
            };

            IActionResult result = await _controller.Put(_dto.ID, newProjectTaskDTO);

            newProjectTaskDTO.ID = new Guid(_dto.ID);
            result.Should().BeOfType<OkObjectResult>()
                  .Which.Value.Should().BeEquivalentTo(newProjectTaskDTO);
        }

        [Fact(DisplayName = "Given a non-existing id and ObjectDTO, " +
                            "When putting a Object, " +
                            "Then NotFound is returned.")]
        public async Task PutNonExistingDTO() {
            IActionResult result = await _controller.Put(_dto.ID, _dto.Get());

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact(DisplayName = "Given a existing id, " +
                            "When deleting a Object, " +
                            "Then Ok is returned.")]
        public async Task DeleteDTOWithExistingID() {
            await _controller.Post(_dto.Get());
            IActionResult result = await _controller.Delete(_dto.ID);

            result.Should().BeOfType<OkResult>();
        }

        [Fact(DisplayName = "Given a non-existing id, " +
                            "When deleting a Object, " +
                            "Then NotFound is returned.")]
        public async Task DeleteDTOWithNonExistingID() {
            IActionResult result = await _controller.Delete(_dto.ID);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}