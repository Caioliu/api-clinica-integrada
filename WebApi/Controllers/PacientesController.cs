﻿using Application.DTOs;
using Application.Handlers.Agendamentos.Queries.GetAgendamentoById;
using Application.Handlers.Consultas.Queries.GetConsultaById;
using Application.Handlers.Pacientes.Commands.Create;
using Application.Handlers.Pacientes.Commands.Delete;
using Application.Handlers.Pacientes.Commands.Update;
using Application.Handlers.Pacientes.Queries.GetPacienteById;
using Application.Handlers.Pacientes.Queries.GetPacientes;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/pacientes")]
    [ApiController]
    public class PacientesController : ApiControllerBase
    {
        [Authorize(Roles = "atendente")]
        [HttpGet]
        public async Task<ActionResult<PaginatedList<PacienteDTO>>> Get([FromQuery] GetPacientesQuery query) {
            return Ok(await Mediator.Send(query));
        }

        [Authorize(Roles = "atendente")]
        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteDTO>> GetById(Guid id) {
            var result = await Mediator.Send(new GetPacienteByIdQuery { Id = id });
            if (!result.Succeeded) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Authorize(Roles = "atendente")]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreatePacienteCommand command) {
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
        [HttpPut("{id}")]
        public async Task<ActionResult<PacienteDTO>> Update(Guid id, [FromBody] UpdatePacienteCommand command) {
            command.Id = id;
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
        public async Task<ActionResult<string>> Delete(Guid id) {
            var result = await Mediator.Send(new DeletePacienteCommand { Id = id });
            if (result.Succeeded) {
                return Ok(result.Data);
            }
            return BadRequest();
        }
    }
}
