using AutoFixture.Xunit2;
using FluentAssertions;
using Igit.Abstractions.Contracts;
using Igit.Abstractions.Models.Requests;
using Igit.Application.Tests.Fixture;
using Igit.Entities.Entities;
using Igit.Postgres;
using Microsoft.Extensions.DependencyInjection;

namespace Igit.Application.Tests;

public class UserServiceTests(ApplicationTestFixtureFactory factory) : IClassFixture<ApplicationTestFixtureFactory>
{
    [Theory(DisplayName = "CreateAsync should insert new user into database"), AutoData]
    public async Task CreateAsync_ShouldInsertNewRecordIntoDb(CreateUserRequest createUserRequest)
    {
        // Arrange
        using var scope = factory.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

        var context = scope.ServiceProvider.GetRequiredService<CoreDbContext>();
        var userRole = context.Set<Role>().First(x => x.Name == "User");

        createUserRequest = createUserRequest with { RoleId = userRole.Id };

        // Act
        var res = await userService.CreateAsync(createUserRequest, CancellationToken.None);

        // Assert
        res.Should().NotBeNull();
        res.RoleName.Should().Be(userRole.Name);
    }

    [Theory(DisplayName = "GetByIdAsync should return user from database"), AutoData]
    public async Task GetByIdAsync_ShouldReturnUserFromDb(CreateUserRequest createUserRequest)
    {
        // Arrange
        using var scope = factory.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

        var context = scope.ServiceProvider.GetRequiredService<CoreDbContext>();
        var userRole = context.Set<Role>().First(x => x.Name == "User");

        createUserRequest = createUserRequest with { RoleId = userRole.Id };

        // Act
        var createRes = await userService.CreateAsync(createUserRequest, CancellationToken.None);
        var getRes = await userService.GetByIdAsync(createRes.Id, CancellationToken.None);

        // Assert
        getRes.Should().NotBeNull();
        getRes.RoleName.Should().Be(userRole.Name);
        getRes.Id.Should().Be(createRes.Id);
    }

    [Theory(DisplayName = "UpdateAsync should update records in database"), AutoData]
    public async Task UpdateAsync_ShouldUpdateRecordFromDb(CreateUserRequest createUserRequest)
    {
        // Arrange
        using var scope = factory.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

        var context = scope.ServiceProvider.GetRequiredService<CoreDbContext>();
        var userRole = context.Set<Role>().First(x => x.Name == "User");

        createUserRequest = createUserRequest with { RoleId = userRole.Id };

        // Act
        var createRes = await userService.CreateAsync(createUserRequest, CancellationToken.None);

        var updateRequest = new UpdateUserRequest
        {
            Id = createRes.Id,
            FullName = "BrandNewName",
            Email = "BrandNewEmail"
        };
        var updateRes = await userService.UpdateAsync(updateRequest, CancellationToken.None);

        // Assert
        updateRes.Should().NotBeNull();
        updateRes.FullName.Should().NotBe(createUserRequest.FullName);
        updateRes.Id.Should().Be(createRes.Id);
        updateRes.RoleName.Should().Be(userRole.Name);
    }
}