using Domain.Common;

namespace Domain.Entities
{
    public class Paciente : AuditableEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public int Idade { get; set; }
        public string? NomeResponsavel { get; set; }
        public string? ParentescoResponsavel { get; set; }
        public string? Observacao { get; set; }
        public bool RecebeuAlta { get; set; }


    }
}
