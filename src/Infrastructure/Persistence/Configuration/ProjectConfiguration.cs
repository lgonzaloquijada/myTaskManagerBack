using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
        builder.Property(p => p.Description).IsRequired().HasMaxLength(512);
        builder.Property(p => p.StartDate).IsRequired();
        builder.Property(p => p.EndDate).IsRequired();
        builder.Property(p => p.Status).IsRequired();
        builder.Property(p => p.Priority).IsRequired();
        builder.Property(p => p.ManagerId).IsRequired();

        builder.HasOne(p => p.Manager).WithMany().HasForeignKey(p => p.ManagerId);
    }
}