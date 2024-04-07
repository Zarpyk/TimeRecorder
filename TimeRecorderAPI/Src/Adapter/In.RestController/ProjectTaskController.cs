using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TimeRecorderAPI.Application.Port.In.Service.ProjectTaskPort;
using TimeRecorderAPI.Exceptions.Responses;
using TimeRecorderAPI.Extensions;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Adapter.In.RestController {
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectTaskController(
        IFindProjectTaskInPort findInPort,
        IAddProjectTaskInPort addInPort,
        IModifyProjectTaskInPort modifyInPort,
        IDeleteProjectTaskInPort deleteInPort,
        IValidator<ProjectTaskDTO> validator
    ) : GenericController<ProjectTaskDTO>(findInPort, addInPort, modifyInPort, deleteInPort, validator) {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProjectDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public override async Task<IActionResult> GetAll() {
            return await base.GetAll();
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectTaskDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public override async Task<IActionResult> Get(string id) {
            return await base.Get(id);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProjectTaskDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationExceptionResponse))]
        public override async Task<IActionResult> Post(ProjectTaskDTO dto) {
            return await base.Post(dto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectTaskDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationExceptionResponse))]
        public override async Task<IActionResult> Put(string id, ProjectTaskDTO dto) {
            return await base.Put(id, dto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public override async Task<IActionResult> Delete(string id) {
            return await base.Delete(id);
        }
    }
}