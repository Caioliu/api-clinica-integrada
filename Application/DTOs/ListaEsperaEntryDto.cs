using Application.Mappings;
using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs
{
    public class ListaEsperaEntryDTO : IMapFrom<ListaEspera>
    {
        public Guid Id { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime? DataSaida { get; set; }
        public string Status { get; set; }
        public string Prioridade { get; set; }
        public string Especialidade { get; set; }
        public Guid PacienteId { get; set; }

        public void Mapping(AutoMapper.Profile profile) {
            profile.CreateMap<ListaEspera, ListaEsperaEntryDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.DataEntrada, opt => opt.MapFrom(s => s.DataEntrada))
                .ForMember(d => d.DataSaida, opt => opt.MapFrom(s => s.DataSaida))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status))
                .ForMember(d => d.Prioridade, opt => opt.MapFrom(s => s.Prioridade))
                .ForMember(d => d.PacienteId, opt => opt.MapFrom(s => s.PacienteId))
                .ForMember(d => d.Especialidade, opt => opt.MapFrom(s => s.Especialidade.ToString()))
                ;
        }
    }
}
