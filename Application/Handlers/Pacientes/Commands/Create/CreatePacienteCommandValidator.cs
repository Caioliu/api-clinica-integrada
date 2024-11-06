using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Pacientes.Commands.Create
{
    public class CreatePacienteCommandValidator : AbstractValidator<CreatePacienteCommand>
    {
        public CreatePacienteCommandValidator() {
            RuleFor(v => v.Paciente.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório.");
            RuleFor(v => v.Paciente.Telefone)
                .NotEmpty().WithMessage("Telefone é obrigatório.");
        }
    }
}
