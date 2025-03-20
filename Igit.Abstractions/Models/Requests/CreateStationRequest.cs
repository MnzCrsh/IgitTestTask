namespace Igit.Abstractions.Models.Requests;

public record CreateStationRequest
{
    public string Name { get; init; } = null!;

    public List<CreateEnergyBlockRequest> EnergyBlocks { get; init; } = null!;
}