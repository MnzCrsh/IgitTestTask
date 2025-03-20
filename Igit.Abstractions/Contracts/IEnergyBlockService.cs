using Igit.Abstractions.Models.Requests;
using Igit.Abstractions.Models.Responses;

namespace Igit.Abstractions.Contracts;

/// <summary>
/// Service that provides CRUD operations for energy block
/// </summary>
public interface IEnergyBlockService
{
    /// <summary>
    /// Adds new energy block to database
    /// </summary>
    /// <param name="createEnergyBlockRequest">Request to create energy block</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    public Task<EnergyBlockResponse> CreateAsync(CreateEnergyBlockRequest createEnergyBlockRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Fetches energy block by id
    /// </summary>
    /// <param name="id">Energy block id</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    public Task<EnergyBlockResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Updates energy block
    /// </summary>
    /// <param name="updateEnergyBlockRequest">Request to update energy block</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    public Task<EnergyBlockResponse> UpdateAsync(UpdateEnergyBlockRequest updateEnergyBlockRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Physically deletes energy block from database
    /// </summary>
    /// <param name="id">Energy block id</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}