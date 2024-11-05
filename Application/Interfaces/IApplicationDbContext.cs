using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Usuario> Usuarios { get; set; }
        DbSet<Paciente> Pacientes { get; set; }

        Task RollBack();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());


    }
}
