using Application.DTOs;
using Application.Handlers.Equipes.Queries.GetEquipeById;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        private readonly IDateTime _dateTime;

        public DeletePacienteCommandHandler(IApplicationDbContext context,
                                            IMapper mapper,
                                            IDateTime dateTime

                                            ) 
        {
            _context = context;
            _mapper = mapper;
            _dateTime = dateTime;

        }

        public async Task<ServiceResult<string>> Handle(DeletePacienteCommand request, CancellationToken cancellationToken) 
        {

            try {
                var entity = await _context.Pacientes
                    .Where(p => !p.IsDeleted) 
                    .FirstOrDefaultAsync(p => p.Id == request.Id);

                if (entity == null) {
                    throw new Exception("No se encontró el paciente");
                }

                entity.ExcludedAt = _dateTime.Now;
                entity.IsDeleted = true;
                _context.Pacientes.Update(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return ServiceResult.Success("Ok");

            } catch (Exception e) {
                throw;

            


            }
           
        }

    }

}
