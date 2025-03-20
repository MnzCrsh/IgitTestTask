using Igit.Abstractions.Models.Requests;
using Igit.Abstractions.Models.Responses;

namespace Igit.Abstractions.Contracts;

/// <summary>
/// Service that provides CRUD operations for user
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Creates new user in database
    /// </summary>
    /// <param name="createUserRequest">Request to create user</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    public Task<UserResponse> CreateAsync(CreateUserRequest createUserRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Fetches user by id
    /// </summary>
    /// <param name="id">User id</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    public Task<UserResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Updates user
    /// </summary>
    /// <param name="updateUserRequest">Request to update user</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    public Task<UserResponse> UpdateAsync(UpdateUserRequest updateUserRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Physically deletes user from database
    /// </summary>
    /// <param name="id">User id</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}