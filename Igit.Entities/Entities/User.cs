namespace Igit.Entities.Entities;

/// <summary>
/// User entity
/// </summary>
public record User
{
    public Guid Id { get; set; }

    public string FullName { get; init; } = null!;

    public string Email { get; init; } = null!;

    public Guid RoleId { get; init; }

    public Role Role { get; init; } = null!;
}