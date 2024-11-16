using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Agendamentos.Commands.Create
{
    public class CreateAgendamentoCommand : AgendamentoCommand, IRequest<ServiceResult>
    {

    }

    public class CreateAgendamentoCommandHandler : IRequestHandler<CreateAgendamentoCommand, ServiceResult>
    {
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
                var entity = new Agendamento {
                    DataHoraInicio = request.DataHoraInicio,
                    DataHoraFim = request.DataHoraFim,
                    Tipo = request.Tipo,
                    Status = request.Status,
                    PacienteId = request.PacienteId,
                    //EquipeId = request.EquipeId,
                    SalaId = request.SalaId,
                    ConsultaId = request.ConsultaId
                };

                await _context.Agendamentos.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return ServiceResult.Success("Ok");
            } catch (Exception ex) {
                await _context.RollBack();
                throw;
            }
        }
    }
}
