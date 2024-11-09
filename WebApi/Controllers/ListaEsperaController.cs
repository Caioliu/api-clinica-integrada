using Application.DTOs;
using Application.Handlers.ListaEsperaEntries.Commands.Create;
using Application.Handlers.ListaEsperaEntries.Commands.Delete;
using Application.Handlers.ListaEsperaEntries.Commands.Update;
using Application.Handlers.ListaEsperaEntries.Queries.GetListaEsperaEntries;
using Application.Handlers.ListaEsperaEntries.Queries.GetListaEsperaEntryById;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers
{
    [Route("api/lista-espera")]
    [ApiController]
    public class ListaEsperaController : ApiControllerBase
    {
        //[Authorize(Roles = "atendente")]
        [HttpGet]
        public async Task<ActionResult<PaginatedList<ListaEsperaEntryDto>>> Get([FromQuery] GetListaEsperaEntriesQuery query) {
            return Ok(await Mediator.Send(query));
        }

        //[Authorize(Roles = "atendente")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ListaEsperaEntryDto>> GetById(Guid id) {
            return Ok(await Mediator.Send(new GetListaEsperaEntryByIdQuery { Id = id }));
        }

        //[Authorize(Roles = "atendente")]
        [HttpPost("{pacienteId}")]
        public async Task<ActionResult<ListaEsperaEntryDto>> Create(CreateListaEsperaEntryCommand command, Guid pacienteId) {
            command.PacienteId = pacienteId;
            return Ok(await Mediator.Send(command));
        }

        //[Authorize(Roles = "atendente")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ListaEsperaEntryDto>> Update(Guid id, UpdateListaEsperaEntryCommand command) {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }

        //[Authorize(Roles = "atendente")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(Guid id) {
            return Ok(await Mediator.Send(new DeleteListaEsperaEntryCommand { Id = id }));
        }

    }
}
