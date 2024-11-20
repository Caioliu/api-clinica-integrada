﻿using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Agendamentos.Commands.Create
{
    public class CreateAgendamentoCommand : IRequest<ServiceResult> {
        public AgendamentoCommand Agendamento { get; set; }
        public ConsultaReducedCommand Consulta { get; set; }
    }

    public class CreateAgendamentoCommandHandler : IRequestHandler<CreateAgendamentoCommand, ServiceResult> {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateAgendamentoCommandHandler(
            IApplicationDbContext context,
            IMapper mapper
            ) {
            _context = context;
            _mapper = mapper;

        }

        public async Task<ServiceResult> Handle(CreateAgendamentoCommand request, CancellationToken cancellationToken) {
            try {
                var agendamentoEntity = new Agendamento {
                    DataHoraInicio = request.Agendamento.DataHora,
                    DataHoraFim = request.Agendamento.DataHoraFim,
                    Tipo = request.Agendamento.Tipo,
                    Status = request.Agendamento.Status,
                    PacienteId = request.Agendamento.PacienteId,
                    SalaId = request.Agendamento.SalaId,
                };

                var consultaEntity = new Consulta {
                    Observacao = request.Consulta.Observacao,
                    Especialidade = request.Consulta.Especialidade,
                    Status = ConsultaStatus.Agendada,
                    AgendamentoId = agendamentoEntity.Id,
                    EquipeId = request.Consulta.EquipeId,
                };

                agendamentoEntity.ConsultaId = consultaEntity.Id;

                //Atualizar Status Lista de Espera para Atendido
                await AtualizarStatusListaEspera(request.Agendamento.PacienteId, cancellationToken);

                await _context.Agendamentos.AddAsync(agendamentoEntity, cancellationToken);
                await _context.Consultas.AddAsync(consultaEntity, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                return ServiceResult.Success("Ok");
            } catch (Exception ex) {
                await _context.RollBack();
                throw;
            }
        }

        private async Task AtualizarStatusListaEspera(Guid pacienteId, CancellationToken cancellationToken) {
            var listaEspera = await _context.ListaEspera
                .Where(x => x.PacienteId == pacienteId && x.Status == ListaStatus.Aguardando)
                .OrderByDescending(x => x.DataEntrada)
                .FirstOrDefaultAsync(cancellationToken);

            if (listaEspera != null) {
                listaEspera.Status = ListaStatus.Atendido;
            }
        }


    }
}

