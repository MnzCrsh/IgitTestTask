using AutoFixture.Xunit2;
using FluentAssertions;
using Igit.Abstractions.Contracts;
using Igit.Abstractions.Models.Requests;
using Igit.Application.Tests.Fixture;
using Igit.Entities.Entities;
using Igit.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Igit.Application.Tests;

public class StationServiceTests(ApplicationTestFixtureFactory factory) : IClassFixture<ApplicationTestFixtureFactory>
{
    [Theory(DisplayName = "CreateAsync should insert new record into database"), AutoData]
    public async Task CreateAsync_ShouldInsertNewRecordIntoDb(CreateStationRequest createRequest)
    {
        // Arrange
        using var scope = factory.CreateScope();
        var stationService = scope.ServiceProvider.GetRequiredService<IStationService>();

        // Act
        var res = await stationService.CreateAsync(createRequest, CancellationToken.None);

        // Assert
        res.Should().NotBeNull();
        res.Id.Should().NotBeEmpty();
        res.EnergyBlocks.Should().NotBeNull();
        res.Name.Should().NotBeEmpty().And.BeEquivalentTo(createRequest.Name);
    }

    [Theory(DisplayName = "GetyIdAsync should fetch record from database"), AutoData]
    public async Task GetByIdAsync_ShouldFetchStationFromDb(CreateStationRequest createRequest)
    {
        // Arrange
        using var scope = factory.CreateScope();
        var stationService = scope.ServiceProvider.GetRequiredService<IStationService>();

        // Act
        var createRes = await stationService.CreateAsync(createRequest, CancellationToken.None);
        var getRes = await stationService.GetByIdAsync(createRes.Id, CancellationToken.None);

        // Assert
        getRes.Should().NotBeNull();
        getRes.Id.Should().NotBeEmpty().And.Be(createRes.Id);
        getRes.EnergyBlocks.Should().NotBeNull();
        getRes.Name.Should().NotBeEmpty().And.BeEquivalentTo(createRes.Name);
    }

    [Theory(DisplayName = "UpdateAsync should update record in database"), AutoData]
    public async Task UpdateAsync_ShouldUpdateStationInDb(CreateStationRequest createRequest)
    {
        // Arrange
        using var scope = factory.CreateScope();
        var stationService = scope.ServiceProvider.GetRequiredService<IStationService>();

        // Act
        var createRes = await stationService.CreateAsync(createRequest, CancellationToken.None);

        var updateRequest = new UpdateStationRequest { Id = createRes.Id, Name = "NewName" };
        var updateRes = await stationService.UpdateAsync(updateRequest, CancellationToken.None);

        // Assert
        updateRes.Should().NotBeNull();
        updateRes.Id.Should().Be(createRes.Id);
        updateRes.Name.Should().NotBe(createRes.Name).And.BeEquivalentTo(updateRequest.Name);
        updateRes.EnergyBlocks.Should().NotBeNull();
    }

    [Theory(DisplayName = "DeleteAsync should delete record from database"), AutoData]
    public async Task DeleteAsync_ShouldDeleteStationFromDb(CreateStationRequest createRequest)
    {
        // Arrange
        using var scope = factory.CreateScope();
        var stationService = scope.ServiceProvider.GetRequiredService<IStationService>();
        var context = scope.ServiceProvider.GetRequiredService<CoreDbContext>();

        // Act
        var createRes = await stationService.CreateAsync(createRequest, CancellationToken.None);

        await stationService.DeleteAsync(createRes.Id, CancellationToken.None);

        var stationFromDb = await context.Set<Station>().FirstOrDefaultAsync(x => x.Id == createRes.Id);

        // Assert
        stationFromDb.Should().BeNull();
    }
}