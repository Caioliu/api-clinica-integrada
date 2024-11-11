using Application.DTOs;
using Application.Handlers.Equipes.Commands.Create;
using Application.Handlers.Equipes.Queries.GetEquipes;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/equipes")]
    [ApiController]
    public class EquipesController : ApiControllerBase
    {
        //Authorize(Roles = "atendente")
        [HttpGet]
        public async Task<ActionResult<PaginatedList<EquipeDto>>> Get([FromQuery] GetEquipesQuery query) {
            return Ok(await Mediator.Send(query));
        }

        //[Authorize(Roles = "atendente")]
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateEquipe(CreateEquipeCommand command) {
            return Ok(await Mediator.Send(command));
        }



    }
}
