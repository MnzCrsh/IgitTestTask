using AutoMapper;
using Igit.Abstractions.Contracts;
using Igit.Abstractions.Models.Requests;
using Igit.Abstractions.Models.Responses;
using Igit.Entities.Entities;
using Igit.Postgres;
using Microsoft.EntityFrameworkCore;

namespace Igit.Application.Services;

/// <inheritdoc/>
internal class UserService(CoreDbContext context, IMapper mapper) : IUserService
{
    /// <inheritdoc/>
    public async Task<UserResponse> CreateAsync(CreateUserRequest createUserRequest, CancellationToken cancellationToken)
    {
        var mappedRequest = mapper.Map<User>(createUserRequest);
        mappedRequest.Id = Guid.CreateVersion7();

        await context.Set<User>().AddAsync(mappedRequest, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserResponse>(mappedRequest);
    }

    /// <inheritdoc/>
    public async Task<UserResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var res = await context.Set<User>()
                      .Include(u => u.Role)
                      .AsNoTracking()
                      .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
                  ?? throw new ArgumentException($"Fetch Error: User with ID[{id}] not found");

        return mapper.Map<UserResponse>(res);
    }

    /// <inheritdoc/>
    public async Task<UserResponse> UpdateAsync(UpdateUserRequest updateUserRequest, CancellationToken cancellationToken)
    {
        var existingUser = await context.Set<User>()
                               .FirstOrDefaultAsync(x => x.Id == updateUserRequest.Id, cancellationToken)
                           ?? throw new ArgumentException($"Update Error: User with ID[{updateUserRequest.Id}] not found");

        mapper.Map(updateUserRequest, existingUser);
        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserResponse>(existingUser);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken) =>
        await context.Set<User>().ExecuteDeleteAsync(cancellationToken);
}