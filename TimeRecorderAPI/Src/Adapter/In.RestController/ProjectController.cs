using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TimeRecorderAPI.Application.Port.In.Service.ProjectPort;
using TimeRecorderAPI.Exceptions.Responses;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Adapter.In.RestController {
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController(
        IFindProjectInPort findInPort,
        IAddProjectInPort addInPort,
        IModifyProjectInPort modifyInPort,
        IDeleteProjectInPort deleteInPort,
        IValidator<ProjectDTO> validator
    ) : GenericController<ProjectDTO>(findInPort, addInPort, modifyInPort, deleteInPort, validator) {

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public override async Task<IActionResult> Get(string id) {
            return await base.Get(id);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProjectDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationExceptionResponse))]
        public override async Task<IActionResult> Post(ProjectDTO dto) {
            return await base.Post(dto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationExceptionResponse))]
        public override async Task<IActionResult> Put(string id, ProjectDTO dto) {
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