using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Consulta : AuditableEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Observacao { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime? DataHoraFim { get; set; }
        public Especialidade Especialidade { get; set; }
        public ConsultaStatus Status { get; set; }

        //Relacionamentos
        public Guid AgendamentoId { get; set; }
        public Agendamento Agendamento { get; set; }
        public Guid EquipeId { get; set; }
        public Equipe Equipe { get; set; }
    }
}
