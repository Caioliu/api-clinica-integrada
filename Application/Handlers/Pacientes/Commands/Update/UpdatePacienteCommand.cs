using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Handlers.Pacientes.Commands.Update
{
    public class UpdatePacienteCommand : PacienteCommand, IRequest<PacienteDTO>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }

    public class UpdatePacienteCommandHandler : IRequestHandler<UpdatePacienteCommand, PacienteDTO>
    {

        private readonly ISender _mediator;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;


        public UpdatePacienteCommandHandler(IApplicationDbContext context,
            IMapper mapper,
            ISender mediator
            ) {
            _mediator = mediator;
            _context = context;
            _mapper = mapper;
        }
        public async Task<PacienteDTO> Handle(UpdatePacienteCommand request, CancellationToken cancellationToken) {
            try {
                var entidadeAlterado = await _context.Pacientes.FindAsync(request.Id);
                //var entidadeOriginal = (Paciente)entidadeAlterado.Clone();

                if (entidadeAlterado == null) {
                    throw new Exception(nameof(Paciente));
                }

                entidadeAlterado.Nome = request.Nome;
                entidadeAlterado.Telefone = request.Telefone;
                entidadeAlterado.Idade = request.Idade;
                entidadeAlterado.NomeResponsavel = request.NomeResponsavel;
                entidadeAlterado.ParentescoResponsavel = request.ParentescoResponsavel;
                entidadeAlterado.Observacao = request.Observacao;
                entidadeAlterado.RecebeuAlta = request.RecebeuAlta;

                //var historico = entidadeOriginal.GerarHistoricoDiferenca(entidadeAlterado, entidadeAlterado.Id, _currentUserService.UserId);
                //await _context.Historicos.AddAsync(historico, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                var result = _mapper.Map<PacienteDTO>(entidadeAlterado);

                return result;
            } catch (Exception ex) {
                throw;
            }
        }

    }

}

