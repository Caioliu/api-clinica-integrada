using Application.Interfaces;
using FluentValidation;

namespace Application.Handlers.Pacientes.Commands.Update
{
    public class UpdatePacienteCommandValidator : PacienteCommandValidator<UpdatePacienteCommand>
    {
        protected readonly IApplicationDbContext _context;

        public UpdatePacienteCommandValidator(IApplicationDbContext context)
            : base(context) 
        {
            context = _context;

            //RuleFor(v => v.Id)
            //    .NotEmpty().WithMessage("Id é obrigatório.");
        }

    }
}
