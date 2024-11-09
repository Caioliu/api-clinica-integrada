using Application.DTOs;
using Application.Handlers.ListaEsperaEntries.Commands;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Handlers.ListaEsperaEntries.Commands.Create
{
    public class CreateListaEsperaEntryCommand : ListaEsperaEntryCommand, IRequest<ListaEsperaEntryDto>
    {

    }

    public class CreateListaEsperaEntryCommandHandler : IRequestHandler<CreateListaEsperaEntryCommand, ListaEsperaEntryDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateListaEsperaEntryCommandHandler(IApplicationDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ListaEsperaEntryDto> Handle(CreateListaEsperaEntryCommand request, CancellationToken cancellationToken) {
            var entity = new Domain.Entities.ListaEspera {
                DataEntrada = request.DataEntrada,
                DataSaida = request.DataSaida,
                Status = request.Status,
                Prioridade = request.Prioridade,
                PacienteId = request.PacienteId
            };

            await _context.ListaEspera.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ListaEsperaEntryDto>(entity);
        }
    }

}
