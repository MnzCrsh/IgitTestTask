namespace Igit.Abstractions.Models.Requests;

public record LoginRequest
{
    public string Email { get; init; } = null!;

    public Guid UserId {get; init; }
}