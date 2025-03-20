namespace Igit.Entities.Entities;

/// <summary>
/// User role entity
/// </summary>
public record Role
{
    public Guid Id { get; init; }

    public string Name { get; init; } = null!;
}