using Application.Interfaces;
using Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Agendamentos.Commands.Delete
{
    public class DeleteAgendamentoCommand : IRequestWrapper<string>
    {
        public Guid Id { get; set; }
    }

    public class DeleteAgendamentoCommandHandler : IRequestHandlerWrapper<DeleteAgendamentoCommand, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTime _dateTime;

        public DeleteAgendamentoCommandHandler(IApplicationDbContext context,
                                        IDateTime dateTime
                                        ) {
            _context = context;
            _dateTime = dateTime;
        }

        public async Task<ServiceResult<string>> Handle(DeleteAgendamentoCommand request, CancellationToken cancellationToken) {
            try {
                var entity = await _context.Agendamentos
                    .Where(p => !p.IsDeleted)
                    .FirstOrDefaultAsync(p => p.Id == request.Id);

                if (entity == null) {
                    throw new Exception("Agendamento não encontrado");
                }

                entity.ExcludedAt = _dateTime.Now;
                entity.IsDeleted = true;
                _context.Agendamentos.Update(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return ServiceResult.Success("Ok");
            } catch (Exception e) {
                throw;
            }
        }
    }

}
