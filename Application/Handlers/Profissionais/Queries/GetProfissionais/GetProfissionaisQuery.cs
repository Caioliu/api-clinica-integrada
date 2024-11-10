using Application.DTOs;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Gridify;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Profissionais.Queries.GetProfissionais
{
    public class GetProfissionaisQuery : GridifyQuery, IRequestWrapper<PaginatedList<ProfissionalDto>>
    {
        public class GetProfissionaisHandler : IRequestHandlerWrapper<GetProfissionaisQuery, PaginatedList<ProfissionalDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetProfissionaisHandler(IApplicationDbContext context, IMapper mapper) {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ServiceResult<PaginatedList<ProfissionalDto>>> Handle(GetProfissionaisQuery request, CancellationToken cancellationToken) {

                var mapper = new GridifyMapper<Profissional>()
                    .GenerateMappings();

                var gridifyQueryable = _context.Profissionais
                    .Where(p => !p.IsDeleted)
                    .GridifyQueryable(request, mapper);

                var query = gridifyQueryable.Query;
                var result = query.AsNoTracking().ToList();

                var resultDTO = _mapper.Map<List<ProfissionalDto>>(result);

                PaginatedList<ProfissionalDto> profissionais = new PaginatedList<ProfissionalDto>(resultDTO, gridifyQueryable.Count, request.Page, request.PageSize);
                return ServiceResult.Success(profissionais);
            }
        }


    }


}
