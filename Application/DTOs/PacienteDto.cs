using Application.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.DTOs
{
    public class PacienteDto : IMapFrom<Paciente>
    {
        public string Nome { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<Paciente, PacienteDto>()
                .ForMember(d => d.Nome, opt => opt.MapFrom(s => s.Nome));
        }
    }
}
