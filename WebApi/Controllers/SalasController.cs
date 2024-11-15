using Application.DTOs;
using Application.Handlers.Salas.Commands.Create;
using Application.Handlers.Salas.Queries.GetSalas;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/salas")]
    [ApiController]
    public class SalasController : ApiControllerBase
    {
        //[Authorize(Roles = "atendente")]
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] GetSalasQuery query) {
            return Ok(await Mediator.Send(query));
        }

        //[Authorize(Roles = "atendente")]
        [HttpPost]
        public async Task<ServiceResult> Create([FromBody] CreateSalaCommand command) {
            return await Mediator.Send(command);

        }
    }
}
