using Application.DTOs;
using Application.Handlers.Consultas.Commands.Create;
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

        //[Authorize(Roles = "atendente")]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateConsultaCommand command) {
            var result = await Mediator.Send(command);
            if (!result.Succeeded) {
                return BadRequest(result);
            }
            return Ok(result);
        }   
    }
}
