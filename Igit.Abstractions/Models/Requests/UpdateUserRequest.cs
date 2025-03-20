namespace Igit.Abstractions.Models.Requests;

public class UpdateUserRequest
{
    public Guid Id { get; init; }

    public string FullName { get; init; } = null!;

    public string Email { get; init; } = null!;

    public Guid? RoleId { get; init; }
}