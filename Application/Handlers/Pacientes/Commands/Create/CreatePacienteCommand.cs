using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Handlers.Pacientes.Commands.Create
{
    public class CreatePacienteCommand : PacienteCommand, IRequest<PacienteDto>
    {

    }

    public class CreatePacienteCommandHandler : IRequestHandler<CreatePacienteCommand, PacienteDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreatePacienteCommandHandler(IApplicationDbContext context,
            IMapper mapper
            ) 
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PacienteDto> Handle(CreatePacienteCommand request, CancellationToken cancellationToken) {
            try {
                var entity = new Paciente {
                    Nome = request.Nome,
                    Telefone = request.Telefone,
                    Idade = request.Idade,
                    NomeResponsavel = request.NomeResponsavel,
                    ParentescoResponsavel = request.ParentescoResponsavel,
                    Observacao = request.Observacao,
                    RecebeuAlta = false

                };

                await _context.Pacientes.AddAsync(entity, cancellationToken);
                //var historico = entity.GerarHistoricoPersonalizado("Registro Incluído", TipoAcaoHistorico.Incluido, _currentUserService.CurrentUser.Id);
                //await _context.Historicos.AddAsync(historico, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return _mapper.Map<PacienteDto>(entity);
            } catch (Exception ex) {
                await _context.RollBack();
                throw;
            }

        }
    }
}
