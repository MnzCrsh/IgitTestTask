namespace Igit.Abstractions.Models.Requests;

public record UpdateStationRequest
{
    public Guid Id { get; init; }

    public string Name { get; init; } = null!;

    public List<UpdateEnergyBlockRequest>? EnergyBlocks { get; init; }
}