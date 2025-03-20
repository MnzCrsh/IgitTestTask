using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Igit.Postgres;

public static class PostgresExtensions
{
    /// <summary>
    /// Adds postgres-related services
    /// </summary>
    public static IServiceCollection AddPostgres(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<CoreDbContext>(options => options.UseNpgsql(connectionString));
        return services;
    }
}