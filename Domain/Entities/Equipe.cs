using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Equipe : AuditableEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public TipoEspecialidade Especialidade { get; set; }


        //Relacionamentos
        public IList<EquipeProfissional> Profissionais { get; set; }
    }
}
