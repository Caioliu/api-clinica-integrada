using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Pacientes.Commands.Create
{
    public class CreatePacienteCommandValidator : PacienteCommandValidator<CreatePacienteCommand>
    {
        public CreatePacienteCommandValidator(IApplicationDbContext context) : base(context) {
            RuleFor(v => v.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório.");
        }
    }
}
