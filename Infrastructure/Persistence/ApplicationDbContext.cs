using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Reflection;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<Usuario, Perfil, Guid>, IApplicationDbContext
    {

        private readonly IDomainEventService _domainEventService;

        protected IDbContextTransaction _contextoTransaction { get; set; }


        public ApplicationDbContext(
            DbContextOptions options,
            IDomainEventService domainEventService
            ) : base(options)
        {
            _domainEventService = domainEventService;

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

        public async Task RollBack() {
            if (_contextoTransaction != null) {
                await _contextoTransaction.RollbackAsync();
            }
        }

        private async Task Commit() {
            if (_contextoTransaction != null) {
                await _contextoTransaction.CommitAsync();
                await _contextoTransaction.DisposeAsync();
                _contextoTransaction = null;
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken()) {
            try {
                var result = await base.SaveChangesAsync(cancellationToken);

                if (_contextoTransaction != null)
                    await Commit();

                await DispatchEvents();

                return result;
            } catch (Exception ex) {
                await RollBack();
                throw;
            }
        }

        private async Task DispatchEvents() {
            while (true) {
                var domainEventEntity = ChangeTracker.Entries<IHasDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .Where(domainEvent => !domainEvent.IsPublished)
                    .FirstOrDefault();
                if (domainEventEntity == null) break;

                domainEventEntity.IsPublished = true;
                await _domainEventService.Publish(domainEventEntity);
            }
        }



    }
}
