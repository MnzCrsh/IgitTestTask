using Igit.Api.Controllers;
using Igit.Application;
using Igit.Core;
using Igit.Mapping;
using Igit.Postgres;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables();

var postgresConfig = builder.Configuration.GetSection("Postgres");

if (builder.Environment.EnvironmentName != "Testing")
{
    builder.Services
        .AddPostgres(postgresConfig["ConnectionString"])
        .AddMappingProfiles()
        .AddIgitSwaggerGen()
        .AddIgitAuthentication()
        .AddAuthorization()
        .AddApplicationServices();
    
    builder.Services
        .AddControllers()
        .AddApplicationPart(typeof(StationController).Assembly);
}

var app = builder.Build();

if (builder.Environment.EnvironmentName != "Testing")
{
    app.UseAuthentication();
    app.UseAuthorization();

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();

    app.MapControllers();
}

app.Run();



/// <summary>
/// Used as work-around for tests
/// </summary>
public partial class Program;