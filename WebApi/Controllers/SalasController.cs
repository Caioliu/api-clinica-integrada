using Application.DTOs;
using Application.Handlers.Salas.Commands.Create;
using Application.Handlers.Salas.Commands.Delete;
using Application.Handlers.Salas.Commands.Update;
using Application.Handlers.Salas.Queries.GetSalaById;
using Application.Handlers.Salas.Queries.GetSalas;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/salas")]
    [ApiController]
    public class SalasController : ApiControllerBase
    {
        [Authorize(Roles = "atendente")]
        [HttpGet]
        public async Task<ActionResult<PaginatedList<SalaDTO>>> Get([FromQuery] GetSalasQuery query) {
            return Ok(await Mediator.Send(query));
        }

        [Authorize(Roles = "atendente")]
        [HttpGet("{id}")]
        public async Task<ActionResult<SalaDTO>> GetById(Guid id) {
            var result = await Mediator.Send(new GetSalaByIdQuery { Id = id });
            if (!result.Succeeded) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Authorize(Roles = "atendente")]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateSalaCommand command) {
            var result = await Mediator.Send(command);
            if (!result.Succeeded) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Authorize(Roles = "atendente")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id) {
            var result = await Mediator.Send(new DeleteSalaCommand { Id = id });
            if (!result.Succeeded) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Authorize(Roles = "atendente")]
        [HttpPut("{id}")]
        public async Task<ActionResult<SalaDTO>> Update(Guid id, [FromBody] UpdateSalaCommand command) {
            command.Id = id;
            var result = await Mediator.Send(command);
            if (!result.Succeeded) {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
