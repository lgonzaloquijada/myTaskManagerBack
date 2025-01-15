using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationRegistrationExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
    }
}