using System.Diagnostics.CodeAnalysis;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

[ExcludeFromCodeCoverage]
public static class ApplicationRegistrationExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IAuthService, AuthService>();
    }
}