using Igit.Mapping.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace Igit.Mapping;

public static class MappingExtensions
{
    public static IServiceCollection AddMappingProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(StationMappingProfile));
        services.AddAutoMapper(typeof(EnergyBlockMappingProfile));
        services.AddAutoMapper(typeof(UserMappingProfile));

        return services;
    }
}