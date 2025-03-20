using FluentAssertions;
using Igit.Abstractions.Contracts;
using Igit.Abstractions.Models.Requests;
using Igit.Application.Tests.Fixture;
using Igit.Entities.Entities;
using Igit.Postgres;
using Microsoft.Extensions.DependencyInjection;

namespace Igit.Application.Tests;

public class AuthenticationServiceTests(ApplicationTestFixtureFactory factory) : IClassFixture<ApplicationTestFixtureFactory>
{
    [Fact(DisplayName = "AuthenticateUserAsync should return bearer token for valid user")]
    public async Task AuthenticateUserAsync_ShouldReturnBearerToken()
    {
        // Arrange
        using var scope = factory.CreateScope();
        var authService = scope.ServiceProvider.GetRequiredService<IAuthenticationService>();
        var context = scope.ServiceProvider.GetRequiredService<CoreDbContext>();

        var userId = new Guid("4783BB3A-F0E7-454C-97FF-E5863FBD7581");
        var userRoleId = context.Set<Role>().First(x => x.Name == "User").Id;
        const string userEmail = "test@test.com";

        await context.Set<User>().AddAsync(new User
        {
            Id = userId,
            FullName = "Test User",
            Email = userEmail,
            RoleId = userRoleId,
        });
        await context.SaveChangesAsync();

        // Act
        var res = await authService.AuthenticateUserAsync(new LoginRequest { Email = userEmail, UserId = userId });

        // Assert
        res.Should().NotBeNull();
    }
}