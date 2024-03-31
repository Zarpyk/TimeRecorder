using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TimeRecorderAPI.Application.Port.In.Service.GenericPort;
using TimeRecorderAPI.Extensions;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPI.Adapter.In.RestController {
    public class GenericController<T>(
        IGenericFindInPort<T> findInPort,
        IGenericAddInPort<T> addInPort,
        IGenericModifyInPort<T> modifyInPort,
        IGenericDeleteInPort<T> deleteInPort,
        IValidator<T> validator
    ) : ControllerBase where T : IDTO {
        public virtual async Task<IActionResult> Get(string id) {
            T? result = await findInPort.Find(id);
            return result == null ? NotFound() : Ok(result);
        }

        public virtual async Task<IActionResult> Post(T dto) {
            await validator.ValidateData(dto);

            T result = await addInPort.Add(dto);

            return CreatedAtAction(nameof(Get), new { id = result.ID }, result);
        }

        public virtual async Task<IActionResult> Put(string id, T dto) {
            await validator.ValidateData(dto);

            T? result = await modifyInPort.Replace(id, dto);

            if (result == null) return NotFound();
            return Ok(result);
        }

        public virtual async Task<IActionResult> Delete(string id) {
            bool result = await deleteInPort.Delete(id);

            if (!result) return NotFound();
            return Ok();
        }
    }
}