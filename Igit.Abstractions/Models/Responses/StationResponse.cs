namespace Igit.Abstractions.Models.Responses;

public record StationResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; } = null!;

    public DateTimeOffset CreatedAt { get; init; }

    public IEnumerable<EnergyBlockResponse> EnergyBlocks { get; init; } = null!;
}