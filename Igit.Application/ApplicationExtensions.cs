using Igit.Abstractions.Contracts;
using Igit.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Igit.Application;

public static class ApplicationExtensions
{
    /// <summary>
    /// Adds domain logic related services
    /// </summary>
    /// <param name="services">IServiceCollection</param>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services
            .AddScoped<IStationService, StationService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IEnergyBlockService, EnergyBlockService>()
            .AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}