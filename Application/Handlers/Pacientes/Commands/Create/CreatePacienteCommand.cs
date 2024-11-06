using Application.DTOs;
using Application.Handlers.ListaEspera.Commands;
using Application.Handlers.ListaEspera.Commands.Create;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Handlers.Pacientes.Commands.Create
{
    public class CreatePacienteCommand : IRequest<ServiceResult>
    {
        public PacienteCommand Paciente { get; set; }
        public ListaEsperaEntryCommand ListaEspera { get; set; }
    }

    public class CreatePacienteCommandHandler : IRequestHandler<CreatePacienteCommand, ServiceResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreatePacienteCommandHandler(IApplicationDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult> Handle(CreatePacienteCommand request, CancellationToken cancellationToken) {
            try {
                var entity = new Paciente {
                    Nome = request.Paciente.Nome,
                    Telefone = request.Paciente.Telefone,
                    Idade = request.Paciente.Idade,
                    NomeResponsavel = request.Paciente.NomeResponsavel,
                    ParentescoResponsavel = request.Paciente.ParentescoResponsavel,
                    Observacao = request.Paciente.Observacao,
                    RecebeuAlta = request.Paciente.RecebeuAlta
                };

                await _context.Pacientes.AddAsync(entity, cancellationToken);

                if (request.ListaEspera != null) {
                    var listaEsperaEntity = new Domain.Entities.ListaEspera {
                        DataEntrada = request.ListaEspera.DataEntrada,
                        DataSaida = request.ListaEspera.DataSaida,
                        Status = request.ListaEspera.Status,
                        Prioridade = request.ListaEspera.Prioridade,
                        PacienteId = entity.Id
                    };

                    await _context.ListaEspera.AddAsync(listaEsperaEntity, cancellationToken);
                }

                await _context.SaveChangesAsync(cancellationToken);

                return ServiceResult.Success("Ok");
            } catch (Exception ex) {
                await _context.RollBack();
                throw;
            }
        }
    }

}
