using Application.DTOs;
using Application.Handlers.Consultas.Commands.Create;
using Application.Handlers.Consultas.Commands.Delete;
using Application.Handlers.Consultas.Commands.Update;
using Application.Handlers.Consultas.Queries.GetConsultaById;
using Application.Handlers.Consultas.Queries.GetConsultas;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/consultas")]
    [ApiController]
    public class ConsultasController : ApiControllerBase
    {
        //[Authorize(Roles = "atendente")]
        [HttpGet]
        public async Task<ActionResult<PaginatedList<ConsultaDTO>>> Get([FromQuery] GetConsultasQuery query) {
            return Ok(await Mediator.Send(query));
        }

        //[Authorize(Roles = "atendente")}
        [HttpGet("{id}")]
        public async Task<ActionResult<ConsultaDTO>> GetById(Guid id) {
            var result = await Mediator.Send(new GetConsultaByIdQuery { Id = id });
            if (!result.Succeeded) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        //[Authorize(Roles = "atendente")]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateConsultaCommand command) {
            var result = await Mediator.Send(command);
            if (!result.Succeeded) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        //[Authorize(Roles = "atendente")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id) {
            var result = await Mediator.Send(new DeleteConsultaCommand { Id = id });
            if (!result.Succeeded) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        //[Authorize(Roles = "atendente")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ConsultaDTO>> Update(Guid id, [FromBody] UpdateConsultaCommand command) {
            command.Id = id;
            var result = await Mediator.Send(command);
            if (!result.Succeeded) {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
