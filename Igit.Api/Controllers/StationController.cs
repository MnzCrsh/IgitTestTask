using Igit.Abstractions.Contracts;
using Igit.Abstractions.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Igit.Api.Controllers;

/// <summary>
/// Station api controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class StationController(IStationService stationService) : ControllerBase
{
    /// <summary>
    /// Creates new station
    /// </summary>
    /// <param name="request">Request to create station</param>
    /// <param name="cancellationToken">Cancellation token</param>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateStation([FromBody] CreateStationRequest request,
        CancellationToken cancellationToken)
    {
        var res = await stationService.CreateAsync(request, cancellationToken);
        return Ok(res);
    }

    /// <summary>
    /// Fetches station by id
    /// </summary>
    /// <param name="id">Station id</param>
    /// <param name="cancellationToken">Cancellation token</param>
    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> GetStationById([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        var res = await stationService.GetByIdAsync(id, cancellationToken);
        return Ok(res);
    }

    /// <summary>
    /// Updates existing station
    /// </summary>
    /// <param name="request">Request to update station</param>
    /// <param name="cancellationToken">Cancellation token</param>
    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStation([FromBody] UpdateStationRequest request,
        CancellationToken cancellationToken)
    {
        var res = await stationService.UpdateAsync(request, cancellationToken);
        return Ok(res);
    }

    /// <summary>
    /// Physically deletes station
    /// </summary>
    /// <param name="id">Station id</param>
    /// <param name="cancellationToken">Cancellation token</param>
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteById([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        await stationService.DeleteAsync(id, cancellationToken);
        return Ok();
    }
}