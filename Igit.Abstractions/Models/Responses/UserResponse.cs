namespace Igit.Abstractions.Models.Responses;

public record UserResponse
{
    public Guid Id { get; init; }

    public string FullName { get; init; } = null!;

    public string RoleName { get; init; } = null!;
}