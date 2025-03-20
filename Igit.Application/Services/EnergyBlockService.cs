using AutoMapper;
using Igit.Abstractions.Contracts;
using Igit.Abstractions.Models.Requests;
using Igit.Abstractions.Models.Responses;
using Igit.Entities.Entities;
using Igit.Postgres;
using Microsoft.EntityFrameworkCore;

namespace Igit.Application.Services;

/// <inheritdoc/>
internal class EnergyBlockService(CoreDbContext context, IMapper mapper) : IEnergyBlockService
{
    /// <inheritdoc/>
    public async Task<EnergyBlockResponse> CreateAsync(CreateEnergyBlockRequest createEnergyBlockRequest,
        CancellationToken cancellationToken)
    {
        var mappedRequest = mapper.Map<EnergyBlock>(createEnergyBlockRequest);

        mappedRequest.Id = Guid.CreateVersion7();
        mappedRequest.CreatedAt = DateTimeOffset.UtcNow;

        await context.Set<EnergyBlock>().AddAsync(mappedRequest, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<EnergyBlockResponse>(mappedRequest);
    }

    /// <inheritdoc/>
    public async Task<EnergyBlockResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var res = await context.Set<EnergyBlock>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
                  ?? throw new ArgumentException($"Fetch Error: Energy Block with ID[{id}] not found");

        return mapper.Map<EnergyBlockResponse>(res);
    }

    /// <inheritdoc/>
    public async Task<EnergyBlockResponse> UpdateAsync(UpdateEnergyBlockRequest updateEnergyBlockRequest,
        CancellationToken cancellationToken)
    {
        var existingEnergyBlock = await context.Set<EnergyBlock>()
            .FirstOrDefaultAsync(x => x.Id == updateEnergyBlockRequest.Id, cancellationToken)
                                 ?? throw new ArgumentException($"Update Error: Station with" +
                                                                $" ID[{updateEnergyBlockRequest.Id}] not found");

        mapper.Map(updateEnergyBlockRequest, existingEnergyBlock);
        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<EnergyBlockResponse>(existingEnergyBlock);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken) =>
        await context.Set<EnergyBlock>().Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken);
}