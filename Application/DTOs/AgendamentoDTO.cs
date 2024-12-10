using Application.Mappings;
using Domain.Entities;
using Domain.Enums;
using System;

namespace Application.DTOs
{
    public class AgendamentoDTO : IMapFrom<Agendamento>
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; }
        public string Especialidade { get; set; }
        public string StatusConsulta { get; set; }
        public Guid PacienteId { get; set; }
        public string Sala { get; set; }
        public Guid? SalaId { get; set; }
        public Guid? ConsultaId { get; set; }

        public void Mapping(MappingProfile profile) {
            profile.CreateMap<Agendamento, AgendamentoDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.DataHoraInicio, opt => opt.MapFrom(s => s.DataHoraInicio))
                .ForMember(d => d.DataHoraFim, opt => opt.MapFrom(s => s.DataHoraFim))
                .ForMember(d => d.Tipo, opt => opt.MapFrom(s => s.Tipo))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status))
                .ForMember(d => d.StatusConsulta, opt => opt.MapFrom(s => s.Consulta.Status))
                .ForMember(d => d.Especialidade, opt => opt.MapFrom(s => s.Consulta.Especialidade))
                .ForMember(d => d.PacienteId, opt => opt.MapFrom(s => s.PacienteId))
                .ForMember(d => d.Nome, opt => opt.MapFrom(s => s.Paciente.Nome))
                .ForMember(d => d.Sala, opt => opt.MapFrom(s => s.Sala.Nome))
                .ForMember(d => d.SalaId, opt => opt.MapFrom(s => s.SalaId))
                .ForMember(d => d.ConsultaId, opt => opt.MapFrom(s => s.ConsultaId));
        }
    }
}
