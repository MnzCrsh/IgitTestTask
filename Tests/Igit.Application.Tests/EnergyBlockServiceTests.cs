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

public class EnergyBlockServiceTests(ApplicationTestFixtureFactory factory) : IClassFixture<ApplicationTestFixtureFactory>
{
    [Theory(DisplayName = "CreateAsync should insert new record into database"), AutoData]
    public async Task CreateAsync_ShouldInsertNewRecordIntoDb(CreateStationRequest createStationRequest,
        CreateEnergyBlockRequest createBlockRequest)
    {
        // Arrange
        var scope = factory.CreateScope();
        var stationService = scope.ServiceProvider.GetRequiredService<IStationService>();
        var blockService = scope.ServiceProvider.GetRequiredService<IEnergyBlockService>();

        // Act
        var stationRes = await stationService.CreateAsync(createStationRequest, CancellationToken.None);

        createBlockRequest = createBlockRequest with { StationId = stationRes.Id };
        var energyBlockRes = await blockService.CreateAsync(createBlockRequest, CancellationToken.None);

        // Assert
        energyBlockRes.Should().NotBeNull();
        energyBlockRes.StationId.Should().Be(stationRes.Id);
    }

    [Theory(DisplayName = "GetByIdAsync should fetch record from database"), AutoData]
    public async Task GetByIdAsync_ShouldFetchRecordFromDb(CreateStationRequest createStationRequest,
        CreateEnergyBlockRequest createBlockRequest)
    {
        // Arrange
        var scope = factory.CreateScope();
        var stationService = scope.ServiceProvider.GetRequiredService<IStationService>();
        var blockService = scope.ServiceProvider.GetRequiredService<IEnergyBlockService>();

        // Act
        var stationRes = await stationService.CreateAsync(createStationRequest, CancellationToken.None);

        createBlockRequest = createBlockRequest with { StationId = stationRes.Id };
        var createBlockRes = await blockService.CreateAsync(createBlockRequest, CancellationToken.None);

        var getRes = await blockService.GetByIdAsync(createBlockRes.Id, CancellationToken.None);

        // Assert
        getRes.Should().NotBeNull();
        getRes.Id.Should().Be(createBlockRes.Id);
        getRes.StationId.Should().Be(stationRes.Id);
    }

    [Theory(DisplayName = "UpdateAsync should update record in database"), AutoData]
    public async Task UpdateAsync_ShouldUpdateRecordInDatabase(CreateStationRequest createStationRequest,
        CreateEnergyBlockRequest createBlockRequest)
    {
        // Arrange
        var scope = factory.CreateScope();
        var stationService = scope.ServiceProvider.GetRequiredService<IStationService>();
        var blockService = scope.ServiceProvider.GetRequiredService<IEnergyBlockService>();

        // Act
        var stationRes = await stationService.CreateAsync(createStationRequest, CancellationToken.None);

        createBlockRequest = createBlockRequest with { StationId = stationRes.Id };
        var createEnergyBlockRes = await blockService.CreateAsync(createBlockRequest, CancellationToken.None);

        var updateBlockRequest = new UpdateEnergyBlockRequest { Id = createEnergyBlockRes.Id, Name = "NewName" };
        var updateRes = await blockService.UpdateAsync(updateBlockRequest, CancellationToken.None);

        // Assert
        updateRes.Should().NotBeNull();
        updateRes.Id.Should().Be(createEnergyBlockRes.Id);
        updateRes.Name.Should().NotBe(createEnergyBlockRes.Name).And.Be(updateBlockRequest.Name);
        updateRes.StationId.Should().Be(stationRes.Id);
    }

    [Theory(DisplayName = "DeleteAsync should delete record from database"), AutoData]
    public async Task DeleteAsync_ShouldDeleteRecordFromDatabase(CreateStationRequest createStationRequest,
        CreateEnergyBlockRequest createBlockRequest)
    {
        // Arrange
        var scope = factory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CoreDbContext>();
        var stationService = scope.ServiceProvider.GetRequiredService<IStationService>();
        var blockService = scope.ServiceProvider.GetRequiredService<IEnergyBlockService>();

        // Act
        var stationRes = await stationService.CreateAsync(createStationRequest, CancellationToken.None);

        createBlockRequest = createBlockRequest with { StationId = stationRes.Id };
        var createEnergyBlockRes = await blockService.CreateAsync(createBlockRequest, CancellationToken.None);

        await blockService.DeleteAsync(createEnergyBlockRes.Id, CancellationToken.None);

        var resFromDb = await context.Set<EnergyBlock>().FirstOrDefaultAsync(x => x.Id == createEnergyBlockRes.Id,
            CancellationToken.None);

        // Assert
        resFromDb.Should().BeNull();
    }
}