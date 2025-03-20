namespace Igit.Abstractions.Models.Requests;

public record CreateEnergyBlockRequest
{
    public string Name { get; init; } = null!;

    public Guid StationId { get; init; }

    public int SensorCount { get; init; }

    public DateTimeOffset PlannedMaintenance { get; init; }
}