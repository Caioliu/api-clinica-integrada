using Application.Interfaces;
using Application.Models;
using AutoMapper;

namespace Application.Handlers.Pacientes.Commands.Delete
{
    public class DeletePacienteCommand : IRequestWrapper<string>
    {
        public Guid Id { get; set; }
    }

    public class DeletePacienteCommandHandler : IRequestHandlerWrapper<DeletePacienteCommand, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeletePacienteCommandHandler(IApplicationDbContext context,
                                            IMapper mapper

                                            ) 
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<string>> Handle(DeletePacienteCommand request, CancellationToken cancellationToken) 
        {

            try {
                var entity = await _context.Pacientes.FindAsync(request.Id);

                if (entity == null) {
                    throw new Exception("No se encontró el paciente");
                }

                _context.Pacientes.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return ServiceResult.Success("Ok");

            } catch (Exception e) {
                throw;

            


            }
           
        }

    }

}
