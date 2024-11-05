using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Pacientes.Commands
{
    public class PacienteCommand
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public int Idade { get; set; }
        public string? NomeResponsavel { get; set; }
        public string? ParentescoResponsavel { get; set; }
        public string? Observacao { get; set; }
    }
}
