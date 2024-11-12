using Application.DTOs;
using Application.Handlers.Equipes.Queries.GetEquipeById;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.ListaEsperaEntries.Commands.Delete
{
    public class DeleteListaEsperaEntryCommand : IRequestWrapper<string>
    {
        public Guid Id { get; set; }
    }

    public class DeleteListaEsperaEntryCommandHandler : IRequestHandlerWrapper<DeleteListaEsperaEntryCommand, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDateTime _dateTime;

        public DeleteListaEsperaEntryCommandHandler(IApplicationDbContext context,
                                            IMapper mapper,
                                            IDateTime dateTime

                                            ) {
            _context = context;
            _mapper = mapper;
            _dateTime = dateTime;

        }

        public async Task<ServiceResult<string>> Handle(DeleteListaEsperaEntryCommand request, CancellationToken cancellationToken) {

            try {
                var entity = await _context.ListaEspera
                    //.Where(p => !p.IsDeleted)
                    .FirstOrDefaultAsync(p => p.Id == request.Id);

                if (entity == null) {
                    throw new Exception("No se encontró la entrada de lista de espera");
                }

                //entity.ExcludedAt = _dateTime.Now;
                //entity.IsDeleted = true;
                //_context.ListaEspera.Update(entity);

                _context.ListaEspera.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return ServiceResult.Success("Ok");

            } catch (Exception e) {
                throw;



            }

        }


    }
}
