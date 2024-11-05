using Application.DTOs;
using Application.Interfaces;
using Application.Models;
using AutoMapper;

namespace Application.Handlers.Pacientes.Queries.GetPacienteById
{
    public class GetPacienteByIdQuery : IRequestWrapper<PacienteDto>
    {
        public Guid Id { get; set; }
    }

    public class GetPacienteByIdQueryHandler : IRequestHandlerWrapper<GetPacienteByIdQuery, PacienteDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPacienteByIdQueryHandler(IApplicationDbContext context,
                                            IMapper mapper

                                            ) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<PacienteDto>> Handle(GetPacienteByIdQuery request, CancellationToken cancellationToken) {

            try {
                var entity = await _context.Pacientes.FindAsync(request.Id);

                if (entity == null) {
                    throw new Exception("No se encontró el paciente");
                }

                var mappedEntity = _mapper.Map<PacienteDto>(entity);

                return ServiceResult.Success(mappedEntity);

            } catch (Exception e) {
                throw;



            }

        }
    }
}

