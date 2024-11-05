using Application.DTOs;
using Application.Handlers.Pacientes.Commands.Create;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/pacientes")]
    [ApiController]
    public class PacientesController : ApiControllerBase
    {
        [Authorize(Roles = "atendente")]
        [HttpPost]
        public async Task<ActionResult<PacienteDto>> Create([FromBody] CreatePacienteCommand command) {
            return await Mediator.Send(command);
        }
    }
}
