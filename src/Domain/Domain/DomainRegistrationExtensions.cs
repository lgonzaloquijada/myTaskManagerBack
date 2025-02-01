using Microsoft.Extensions.DependencyInjection;

namespace Domain;

public static class DomainRegistrationExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        return services;
    }
}