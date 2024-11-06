using Application.DTOs;
using Application.Handlers.ListaEsperaEntries.Queries.GetListraEsperaEntries;
using Application.Models;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers
{
    [Route("api/lista-espera")]
    [ApiController]
    public class ListaEsperaController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<ListaEsperaEntryDto>>> Get([FromQuery] GetListaEsperaEntriesQuery query) {
            return Ok(await Mediator.Send(query));
        }
    }
}
