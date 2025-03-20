namespace Igit.Abstractions.Models.Requests;

public record CreateUserRequest
{
    public string FullName { get; init; } = null!;

    public string Email { get; init; } = null!;

    public Guid RoleId { get; init; }
}