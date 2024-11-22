using Application.DTOs;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Consultas.Commands.Update.FinalizarConsulta
{
    public class UpdateFinalizarConsultaCommand : IRequest<ServiceResult>
    {
        public Guid ConsultaId { get; set; }
    }

    public class UpdateFinalizarConsultaCommandHandler : IRequestHandler<UpdateFinalizarConsultaCommand, ServiceResult>
    {
        private readonly ISender _mediator;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UpdateFinalizarConsultaCommandHandler(IApplicationDbContext context,
            IMapper mapper,
            ISender sender) {
            _context = context;
            _mapper = mapper;
            _mediator = sender;
        }
        public async Task<ServiceResult> Handle(UpdateFinalizarConsultaCommand request, CancellationToken cancellationToken) {
            try {
                var consulta = await _context.Consultas
                    .Include(c => c.Agendamento) // Inclui o Agendamento relacionado
                    .FirstOrDefaultAsync(c => c.Id == request.ConsultaId);

                if (consulta == null) {
                    throw new Exception(nameof(Consulta));
                }

                consulta.Status = ConsultaStatus.Concluida;
                consulta.DataHoraFim = DateTime.Now; // O Horário é registrado

                //Liberar Sala
                var salaConsulta = await _context.Salas.FirstOrDefaultAsync(x => x.Id == consulta.Agendamento.SalaId);
                if (salaConsulta != null) {
                    salaConsulta.IsDisponivel = true;
                }
                await _context.SaveChangesAsync(cancellationToken);
                var result = salaConsulta != null ? "Sala Liberada" : "Ok";
                return ServiceResult.Success(result);
            } catch (Exception ex) {
                throw;
            }
        }
    }

}
