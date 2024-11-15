using Application.Mappings;
using Domain.Entities;
using Domain.Enums;
using System;

namespace Application.DTOs
{
    public class AgendamentoDTO : IMapFrom<Agendamento>
    {
        public Guid Id { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime? DataHoraFim { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; }
        public Guid PacienteId { get; set; }
        public Guid? SalaId { get; set; }
        public Guid? ConsultaId { get; set; }

        public void Mapping(MappingProfile profile) {
            profile.CreateMap<Agendamento, AgendamentoDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.DataHoraInicio, opt => opt.MapFrom(s => s.DataHoraInicio))
                .ForMember(d => d.DataHoraFim, opt => opt.MapFrom(s => s.DataHoraFim))
                .ForMember(d => d.Tipo, opt => opt.MapFrom(s => s.Tipo))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status))
                .ForMember(d => d.PacienteId, opt => opt.MapFrom(s => s.PacienteId))
                //.ForMember(d => d.EquipeId, opt => opt.MapFrom(s => s.EquipeId))
                .ForMember(d => d.SalaId, opt => opt.MapFrom(s => s.SalaId))
                .ForMember(d => d.ConsultaId, opt => opt.MapFrom(s => s.ConsultaId));
        }
    }
}
