﻿using Application.DTOs;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Gridify;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Agendamentos.Queries.GetAgendamentos
{
    public class GetAgendamentosQuery : GridifyQuery, IRequestWrapper<PaginatedList<AgendamentoDTO>>
    {
        public class GetAgendamentosHandler : IRequestHandlerWrapper<GetAgendamentosQuery, PaginatedList<AgendamentoDTO>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetAgendamentosHandler(IApplicationDbContext context, IMapper mapper) {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ServiceResult<PaginatedList<AgendamentoDTO>>> Handle(GetAgendamentosQuery request, CancellationToken cancellationToken) {

                var mapper = new GridifyMapper<Agendamento>()
                    .GenerateMappings();

                var gridifyQueryable = _context.Agendamentos
                    .Where(p => !p.IsDeleted)
                    .GridifyQueryable(request, mapper);

                var query = gridifyQueryable.Query;
                var result = query.AsNoTracking().ToList();

                var resultDTO = _mapper.Map<List<AgendamentoDTO>>(result);

                PaginatedList<AgendamentoDTO> agendamentos = new PaginatedList<AgendamentoDTO>(resultDTO, gridifyQueryable.Count, request.Page, request.PageSize);
                return ServiceResult.Success(agendamentos);
            }
        }
    }

    
}

