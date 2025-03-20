namespace Igit.Entities.Entities;

/// <summary>
///  Station entity
/// </summary>
public record Station
{
    public Guid Id { get; set; }

    public string Name { get; init; } = null!;

    public DateTimeOffset CreatedAt { get; set; }

    public IEnumerable<EnergyBlock> EnergyBlocks { get; init; } = null!;
}