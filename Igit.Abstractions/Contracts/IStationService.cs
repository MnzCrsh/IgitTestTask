using Igit.Abstractions.Models.Requests;
using Igit.Abstractions.Models.Responses;

namespace Igit.Abstractions.Contracts;

/// <summary>
/// Service that provides CRUD operations for Station
/// </summary>
public interface IStationService
{
    /// <summary>
    /// Adds new station to database
    /// </summary>
    /// <param name="createStationRequest">Request to create station</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    public Task<StationResponse> CreateAsync(CreateStationRequest createStationRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Fetches station by id
    /// </summary>
    /// <param name="id">Station id</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    public Task<StationResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Updates station
    /// </summary>
    /// <param name="updateStationRequest">Request to update station</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    public Task<StationResponse> UpdateAsync(UpdateStationRequest updateStationRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Physically deletes station from database
    /// </summary>
    /// <param name="id">Station id</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}