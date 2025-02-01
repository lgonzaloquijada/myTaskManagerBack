using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Persistence.Configuration;
public static class EntityConfiguration
{
    public static void Configure(ModelBuilder builder, IMutableEntityType entity)
    {
        var type = entity.ClrType;

        builder.Entity(type).HasKey("Id");
        builder.Entity(type).Property("Id").ValueGeneratedOnAdd();
        builder.Entity(type).Property("CreatedAt").IsRequired();
        builder.Entity(type).Property("UpdatedAt").IsRequired();
        builder.Entity(type).Property("CreatedBy").IsRequired();
        builder.Entity(type).Property("UpdatedBy").IsRequired();
        builder.Entity(type).Property("IsActive").IsRequired();
    }
}