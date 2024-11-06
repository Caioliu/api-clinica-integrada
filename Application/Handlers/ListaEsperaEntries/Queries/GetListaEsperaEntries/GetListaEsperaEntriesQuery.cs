using Application.DTOs;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Gridify;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.ListaEsperaEntries.Queries.GetListraEsperaEntries
{
    public class GetListaEsperaEntriesQuery : GridifyQuery, IRequest<PaginatedList<ListaEsperaEntryDto>>
    {
        public class GetListaEsperaQueryHandler : IRequestHandler<GetListaEsperaEntriesQuery, PaginatedList<ListaEsperaEntryDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetListaEsperaQueryHandler(IApplicationDbContext context, IMapper mapper) {
                _context = context;
                _mapper = mapper;
            }


            public Task<PaginatedList<ListaEsperaEntryDto>> Handle(GetListaEsperaEntriesQuery request, CancellationToken cancellationToken) {
                var mapper = new GridifyMapper<Domain.Entities.ListaEspera>();

                var gridifyQueryable = _context.ListaEspera
                    .Where(p => !p.IsDeleted)
                    .GridifyQueryable(request, mapper);

                var query = gridifyQueryable.Query;
                var result = query.AsNoTracking().ToList();

                var resultDTO = _mapper.Map<List<ListaEsperaEntryDto>>(result);

                PaginatedList<ListaEsperaEntryDto> listaEsperaEntries = new PaginatedList<ListaEsperaEntryDto>(resultDTO, gridifyQueryable.Count, request.Page, request.PageSize);
                return Task.FromResult(listaEsperaEntries);
            }
        }

    }
}
