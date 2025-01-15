using Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceRegistrationExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        return services;
    }
}