namespace Igit.Entities.Entities;

/// <summary>
/// Energy block entity
/// </summary>
public record EnergyBlock
{
    public Guid Id { get; set; }

    public string Name { get; init; } = null!;

    public Guid StationId { get; init; }

    public Station Station { get; init; } = null!;

    public int SensorCount { get; init; }

    public DateTimeOffset PlannedMaintenance { get; init; }

    public DateTimeOffset CreatedAt { get; set; }
}