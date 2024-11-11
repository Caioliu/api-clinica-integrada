
using Application.DTOs;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Gridify;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Equipes.Queries.GetEquipes
{
    public class GetEquipesQuery : GridifyQuery, IRequestWrapper<PaginatedList<EquipeDto>>
    {
        public class GetEquipesQueryHandler : IRequestHandlerWrapper<GetEquipesQuery, PaginatedList<EquipeDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetEquipesQueryHandler(IApplicationDbContext context, IMapper mapper) {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ServiceResult<PaginatedList<EquipeDto>>> Handle(GetEquipesQuery request, CancellationToken cancellationToken) {
                var mapper = new GridifyMapper<Equipe>()
                    .GenerateMappings();

                var gridifyQueryable = _context.Equipes
                    .Where(p => !p.IsDeleted)
                    .Include(e => e.Profissionais)
                        .ThenInclude(ep => ep.Profissional)
                    .GridifyQueryable(request, mapper);

                var query = gridifyQueryable.Query;
                var result = await query.AsNoTracking().ToListAsync(cancellationToken);

                var resultDTO = _mapper.Map<List<EquipeDto>>(result);

                PaginatedList<EquipeDto> equipes = new PaginatedList<EquipeDto>(resultDTO, gridifyQueryable.Count, request.Page, request.PageSize);
                return ServiceResult.Success(equipes);
            }
        }
    }
}
