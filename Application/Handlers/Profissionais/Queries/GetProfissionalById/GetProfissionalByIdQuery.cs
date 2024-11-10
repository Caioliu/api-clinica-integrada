using Application.DTOs;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Profissionais.Queries.GetProfissionalById
{
    public class GetProfissionalByIdQuery : IRequestWrapper<ProfissionalDto>
    {
        public Guid Id { get; set; }
    }

    public class GetProfissionalByIdQueryHandler : IRequestHandlerWrapper<GetProfissionalByIdQuery, ProfissionalDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProfissionalByIdQueryHandler(IApplicationDbContext context,
                                            IMapper mapper

                                            ) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<ProfissionalDto>> Handle(GetProfissionalByIdQuery request, CancellationToken cancellationToken) {

            try {
                var entity = await _context.Profissionais
                                           .Where(p => !p.IsDeleted) // Adiciona a condição para IsDeleted
                                           .FirstOrDefaultAsync(p => p.Id == request.Id);

                if (entity == null) {
                    throw new Exception("No se encontró el profissional");
                }

                var mappedEntity = _mapper.Map<ProfissionalDto>(entity);

                return ServiceResult.Success(mappedEntity);

            } catch (Exception e) {
                throw;
            }
        }
    }
}
