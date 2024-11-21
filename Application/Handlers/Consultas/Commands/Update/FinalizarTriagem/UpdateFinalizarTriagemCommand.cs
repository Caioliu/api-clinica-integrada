using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Handlers.Consultas.Commands.Update.FinalizarTriagem
{
    public class UpdateFinalizarTriagemCommand : IRequest<ServiceResult>
    {
        public Guid ConsultaId { get; set; }
    }

    public class UpdateFinalizarTriagemCommandHandler : IRequestHandler<UpdateFinalizarTriagemCommand, ServiceResult>
    {
        private readonly ISender _mediator;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UpdateFinalizarTriagemCommandHandler(IApplicationDbContext context, IMapper mapper, ISender mediator) {
            _mediator = mediator;
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResult> Handle(UpdateFinalizarTriagemCommand request, CancellationToken cancellationToken) {
            try {
                var consulta = await _context.Consultas.FindAsync(request.ConsultaId);
                if (consulta == null) {
                    throw new Exception(nameof(Consulta));
                }
                consulta.Status = ConsultaStatus.AguardandoConsulta;
                await _context.SaveChangesAsync(cancellationToken);
                return ServiceResult.Success("Triagem Finalizada");
            } catch (Exception ex) {
                throw;
            }
        }
    }
}
