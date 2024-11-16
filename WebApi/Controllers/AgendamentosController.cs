using Application.DTOs;
using Application.Handlers.Agendamentos.Commands.Create;
using Application.Handlers.Agendamentos.Queries.GetAgendamentos;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/agendamentos")]
    [ApiController]
    public class AgendamentosController : ApiControllerBase
    {
        //[Authorize(Roles = "atendente")]
        [HttpGet]
        public async Task<ActionResult<PaginatedList<AgendamentoDTO>>> Get([FromQuery] GetAgendamentosQuery query) {
            return Ok(await Mediator.Send(query));
        }

        //[Authorize(Roles = "atendente")]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateAgendamentoCommand command) {
            var result = await Mediator.Send(command);
            if (!result.Succeeded) {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
