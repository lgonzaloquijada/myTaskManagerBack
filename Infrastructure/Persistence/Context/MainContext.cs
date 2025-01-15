using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context;
public class MainContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public MainContext(DbContextOptions<MainContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MainContext).Assembly);
    }
}
