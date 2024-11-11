using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Equipes.Commands.Create
{
    public class CreateEquipeCommand : IRequest<ServiceResult>
    {
        public TipoEspecialidade Especialidade { get; set; }
        public IList<Guid> Estagiarios { get; set; }
        public IList<Guid> Professores { get; set; }
    }

    public class CreateEquipeCommandHandler : IRequestHandler<CreateEquipeCommand, ServiceResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDateTime _dateTime;

        public CreateEquipeCommandHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IDateTime dateTime
            ) {
            _context = context;
            _mapper = mapper;
            _dateTime = dateTime;

        }


        public async Task<ServiceResult> Handle(CreateEquipeCommand request, CancellationToken cancellationToken) {
            var equipe = new Equipe {
                Id = Guid.NewGuid(),
                Especialidade = request.Especialidade, // Defina a especialidade conforme necessário
                Profissionais = new List<EquipeProfissional>()
            };

            foreach (var estagiarioId in request.Estagiarios) {
                equipe.Profissionais.Add(new EquipeProfissional {
                    EquipeId = equipe.Id,
                    ProfissionalId = estagiarioId
                });
            }

            foreach (var professorId in request.Professores) {
                equipe.Profissionais.Add(new EquipeProfissional {
                    EquipeId = equipe.Id,
                    ProfissionalId = professorId
                });
            }

            _context.Equipes.Add(equipe);
            _context.EquipeProfissional.AddRange(equipe.Profissionais);
            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(equipe.Id);
        }
    }
}
