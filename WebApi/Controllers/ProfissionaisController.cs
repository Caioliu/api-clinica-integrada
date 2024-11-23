using Application.Handlers.Profissionais.Commands.Create;
using Application.Handlers.Profissionais.Commands.Delete;
using Application.Handlers.Profissionais.Commands.Update;
using Application.Handlers.Profissionais.Queries.GetProfissionais;
using Application.Handlers.Profissionais.Queries.GetProfissionalById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/profissionais")]
    [ApiController]
    public class ProfissionaisController : ApiControllerBase
    {
        [Authorize(Roles = "atendente")]
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] GetProfissionaisQuery query) {
            return Ok(await Mediator.Send(query));
        }

        [Authorize(Roles = "atendente")]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateProfissionalCommand command) {
            return Ok(await Mediator.Send(command));
        }

        [Authorize(Roles = "atendente")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id) {
            return Ok(await Mediator.Send(new GetProfissionalByIdQuery { Id = id }));
        }

        [Authorize(Roles = "atendente")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] UpdateProfissionalCommand command) {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }

        [Authorize(Roles = "atendente")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id) {
            var result = await Mediator.Send(new DeleteProfissionalCommand { Id = id });
            if (result.Succeeded) {
                return Ok(result);
            }
            return BadRequest();
        }

    }
}
