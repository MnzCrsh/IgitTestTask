using Igit.Abstractions.Models.Requests;

namespace Igit.Abstractions.Contracts;

/// <summary>
/// Provides access to JWT token
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// Authenticates user by his credentials
    /// </summary>
    /// <param name="request">Login reques</param>
    public Task<string> AuthenticateUserAsync(LoginRequest request);
}