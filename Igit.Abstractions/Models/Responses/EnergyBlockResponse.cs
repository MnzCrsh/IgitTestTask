namespace Igit.Abstractions.Models.Responses;

public record EnergyBlockResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; } = null!;

    public Guid StationId { get; init; }

    public int SensorCount { get; init; }

    public DateTimeOffset PlannedMaintenance { get; init; }

    public DateTimeOffset CreatedAt { get; init; }
}