using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Pacientes.Commands
{
    public class PacienteCommandValidator<T> : AbstractValidator<T> where T : PacienteCommand
    {
        protected readonly IApplicationDbContext _context;

        public PacienteCommandValidator(IApplicationDbContext context) {
            _context = context;

            RuleFor(v => v.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório.");
        }
    }
}
