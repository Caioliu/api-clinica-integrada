using Application.DTOs;
using Application.Handlers.Agendamentos.Commands.Create;
using Application.Handlers.Agendamentos.Commands.Delete;
using Application.Handlers.Agendamentos.Commands.Update;
using Application.Handlers.Agendamentos.Queries.GetAgendamentoById;
using Application.Handlers.Agendamentos.Queries.GetAgendamentos;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/agendamentos")]
    [ApiController]
    public class AgendamentosController : ApiControllerBase
    {
        [Authorize(Roles = "atendente")]
        [HttpGet]
        public async Task<ActionResult<PaginatedList<AgendamentoDTO>>> Get([FromQuery] GetAgendamentosQuery query) {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AgendamentoDTO>> GetById(Guid id) {
            var result = await Mediator.Send(new GetAgendamentoByIdQuery { Id = id });
            if (!result.Succeeded) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Authorize(Roles = "atendente")]
        [HttpPost("consulta-triagem")]
        public async Task<ActionResult> Create([FromBody] CreateAgendamentoCommand command) {
            try {
                var result = await Mediator.Send(command);
                    if (!result.Succeeded) {
                        return BadRequest(result);
                    }
                return Ok(result);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "atendente")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id) {
            var result = await Mediator.Send(new DeleteAgendamentoCommand { Id = id });
            if (!result.Succeeded) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Authorize(Roles = "atendente")]
        [HttpPut("{id}")]
        public async Task<ActionResult<AgendamentoDTO>> Update(Guid id, [FromBody] UpdateAgendamentoCommand command) {
            command.Id = id;
            var result = await Mediator.Send(command);
            if (!result.Succeeded) {
                return BadRequest(result);
            }
            return Ok(result);
        }


    }
}
