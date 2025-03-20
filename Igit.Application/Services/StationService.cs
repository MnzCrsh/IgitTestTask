using AutoMapper;
using Igit.Abstractions.Contracts;
using Igit.Abstractions.Models.Requests;
using Igit.Abstractions.Models.Responses;
using Igit.Entities.Entities;
using Igit.Postgres;
using Microsoft.EntityFrameworkCore;

namespace Igit.Application.Services;

/// <inheritdoc/>
internal class StationService(CoreDbContext context, IMapper mapper) : IStationService
{
    /// <inheritdoc/>
    public async Task<StationResponse> CreateAsync(CreateStationRequest createStationRequest,
        CancellationToken cancellationToken)
    {
        var mappedRequest = mapper.Map<Station>(createStationRequest);

        mappedRequest.Id = Guid.CreateVersion7();
        mappedRequest.CreatedAt = DateTimeOffset.UtcNow;

        await context.Set<Station>().AddAsync(mappedRequest, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<StationResponse>(mappedRequest);
    }

    /// <inheritdoc/>
    public async Task<StationResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var res = await context.Set<Station>()
                      .AsNoTracking()
                      .Include(x => x.EnergyBlocks)
                      .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
                  ?? throw new ArgumentException($"Fetch Error: Station with ID[{id}] not found");

        return mapper.Map<StationResponse>(res);
    }

    /// <inheritdoc/>
    public async Task<StationResponse> UpdateAsync(UpdateStationRequest updateStationRequest,
        CancellationToken cancellationToken)
    {
        var existingStation = await context.Set<Station>()
                                  .Include(x => x.EnergyBlocks)
                                  .FirstOrDefaultAsync(x => x.Id == updateStationRequest.Id, cancellationToken) 
                              ?? throw new ArgumentException
                                  ($"Update Error: Station with ID[{updateStationRequest.Id}] not found");

        mapper.Map(updateStationRequest, existingStation);
        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<StationResponse>(existingStation);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken) =>
        await context.Set<Station>().Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken);
}