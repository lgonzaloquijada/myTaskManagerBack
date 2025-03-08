using Domain.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Configuration;

namespace Persistence.Context;
public class MainContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }

    public MainContext(DbContextOptions<MainContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MainContext).Assembly);

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var type = entity.ClrType;

            if (type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IIsActive)))
            {
                modelBuilder.Entity(type).HasIndex(nameof(IIsActive.IsActive));
            }

            if (type.IsSubclassOf(typeof(Entity)))
            {
                EntityConfiguration.Configure(modelBuilder, entity);
            }
        }
    }

    public override int SaveChanges()
    {
        BeforeSaveChanges();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        BeforeSaveChanges();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void BeforeSaveChanges()
    {
        var entries = ChangeTracker.Entries().Where(x => x.Entity is ITrackableEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            ITrackableEntity entity = (ITrackableEntity)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = DateTime.Now;
                entity.UpdatedAt = DateTime.Now;
                entity.CreatedBy = 1;
                entity.UpdatedBy = 1;
            }
            else if (entry.State == EntityState.Modified)
            {
                entity.UpdatedAt = DateTime.Now;
                entity.UpdatedBy = 1;
            }
        }
    }
}
