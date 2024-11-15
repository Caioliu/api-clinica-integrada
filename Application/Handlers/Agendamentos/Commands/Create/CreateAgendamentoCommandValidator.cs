using Application.Interfaces;

namespace Application.Handlers.Agendamentos.Commands.Create
{
    public class CreateAgendamentoCommandValidator : AgendamentoCommandValidator<CreateAgendamentoCommand>
    {
        protected readonly IApplicationDbContext _context;
        public CreateAgendamentoCommandValidator(IApplicationDbContext context)
            : base(context) {

        }
    }
}
