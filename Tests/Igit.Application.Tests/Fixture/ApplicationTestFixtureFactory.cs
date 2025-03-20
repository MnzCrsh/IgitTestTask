using Igit.Fixture;
using Igit.Mapping;
using Igit.Postgres;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Igit.Application.Tests.Fixture;

public class ApplicationTestFixtureFactory
{
    private readonly ApplicationTestFixtureFactoryImpl _factoryImpl = new();

    public IServiceScope CreateScope() => _factoryImpl.CreateScopeInternal();

    private class ApplicationTestFixtureFactoryImpl : FixtureFactoryBase
    {
        private readonly SqliteConnection _connection = new("DataSource=:memory:");

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            Environment.SetEnvironmentVariable("JWT_KEY", new string('a', 40));

            builder.ConfigureServices(services =>
            {
                services
                    .AddApplicationServices()
                    .AddMappingProfiles();

                services.AddDbContext<CoreDbContext>(options =>
                {
                    options.UseSqlite(_connection);
                });

                _connection.Open();
            });
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            var host = base.CreateHost(builder);

            using var scope = host.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CoreDbContext>();
            dbContext.Database.EnsureCreated();

            return host;
        }

        protected override void Dispose(bool disposing)
        {
            _connection.Dispose();
            base.Dispose(disposing);
        }
    }
}