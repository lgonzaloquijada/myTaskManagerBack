using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace API;

public static class ApiRegistrationExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, ConfigurationManager config)
    {
        services.AddDbContext<MainContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("MainConnection"));
        });
        return services;
    }
}