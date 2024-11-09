using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Handlers.ListaEsperaEntries.Commands.Update
{
    public class UpdateListaEsperaEntryCommand : ListaEsperaEntryCommand, IRequest<ListaEsperaEntryDto>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }

    public class UpdateListaEsperaEntryCommandHandler : IRequestHandler<UpdateListaEsperaEntryCommand, ListaEsperaEntryDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateListaEsperaEntryCommandHandler(IApplicationDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ListaEsperaEntryDto> Handle(UpdateListaEsperaEntryCommand request, CancellationToken cancellationToken) {
            var entity = await _context.ListaEspera.FindAsync(request.Id);

            if (entity == null) {
                throw new Exception(nameof(ListaEspera));
            }

            entity.DataEntrada = request.DataEntrada;
            entity.DataSaida = request.DataSaida;
            entity.Status = request.Status;
            entity.Prioridade = request.Prioridade;
            entity.PacienteId = request.PacienteId;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ListaEsperaEntryDto>(entity);
        }
    }

}
