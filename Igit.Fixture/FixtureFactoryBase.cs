using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Igit.Fixture;

internal class FixtureFactoryBase : WebApplicationFactory<Program>
{
    public IServiceScope CreateScopeInternal() =>
        base.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.UseEnvironment("Testing");
    }
}