using Application.DTOs;
using Application.Handlers.Pacientes.Commands.Create;
using Application.Handlers.Pacientes.Commands.Delete;
using Application.Handlers.Pacientes.Commands.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/pacientes")]
    [ApiController]
    public class PacientesController : ApiControllerBase
    {
        //[Authorize(Roles = "atendente")]
        [HttpPost]
        public async Task<ActionResult<PacienteDto>> Create([FromBody] CreatePacienteCommand command) {
            return await Mediator.Send(command);

        }

        //[Authorize(Roles = "atendente")]
        [HttpPut("{id}")]
        public async Task<ActionResult<PacienteDto>> Update(Guid id, [FromBody] UpdatePacienteCommand command) {
            command.Id = id;
            return await Mediator.Send(command);

        }

        //[Authorize(Roles = "atendente")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(Guid id) {
            var result = await Mediator.Send(new DeletePacienteCommand { Id = id });
            if (result.Succeeded) {
                return Ok(result.Data);
            }
            return BadRequest();
        }
    }
}
