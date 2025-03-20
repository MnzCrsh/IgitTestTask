using Igit.Abstractions.Contracts;
using Igit.Abstractions.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Igit.Api.Controllers;

/// <summary>
/// Energy block api controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class EnergyBlockController(IEnergyBlockService blockService) : ControllerBase
{

    /// <summary>
    /// Creates new energy block
    /// </summary>
    /// <param name="createEnergyBlockRequest">Request to create energy block</param>
    /// <param name="cancellationToken">Cancellation token</param>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateEnergyBlock([FromBody] CreateEnergyBlockRequest createEnergyBlockRequest,
       CancellationToken cancellationToken)
    {
        var res = await blockService.CreateAsync(createEnergyBlockRequest, cancellationToken);
        return Ok(res);
    }

    /// <summary>
    /// Fetches energy block
    /// </summary>
    /// <param name="id">Energy block id</param>
    /// <param name="cancellationToken">Cancellation token</param>
    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> GetEnergyBlockById([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        var res = await blockService.GetByIdAsync(id, cancellationToken);
        return Ok(res);
    }

    /// <summary>
    /// Updates energy block
    /// </summary>
    /// <param name="updateEnergyBlockRequest">Request to update energy block</param>
    /// <param name="cancellationToken">Cancellation token</param>
    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateEnergyBlock([FromBody] UpdateEnergyBlockRequest updateEnergyBlockRequest,
       CancellationToken cancellationToken)
    {
        var res = await blockService.UpdateAsync(updateEnergyBlockRequest, cancellationToken);
        return Ok(res);
    }

    /// <summary>
    /// Physically deletes energy block
    /// </summary>
    /// <param name="id">Energy block id</param>
    /// <param name="cancellationToken">Cancellation token</param>
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteEnergyBlock([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        await blockService.DeleteAsync(id, cancellationToken);
        return Ok();
    }

}