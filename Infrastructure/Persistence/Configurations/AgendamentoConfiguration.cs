using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class AgendamentoConfiguration : IEntityTypeConfiguration<Agendamento>
    {
        public void Configure(EntityTypeBuilder<Agendamento> builder) {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.DataHoraInicio).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.Tipo).IsRequired();

            builder.HasOne(p => p.Paciente)
                .WithMany(p => p.Agendamentos)
                .HasForeignKey(p => p.PacienteId);

            builder.HasOne(a => a.Consulta)
                .WithOne(b => b.Agendamento)
                .HasForeignKey<Agendamento>(a => a.ConsultaId);


        }
    }
}
