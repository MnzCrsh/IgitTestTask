using Igit.Abstractions.Contracts;
using Igit.Abstractions.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Igit.Api.Controllers;

/// <summary>
/// User api controller
/// </summary>
[ApiController]
[Route("/api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{

    /// <summary>
    /// Creates new user
    /// </summary>
    /// <param name="request">Request to create user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var res = await userService.CreateAsync(request, cancellationToken);
        return Ok(res);
    }

    /// <summary>
    /// Fetches user
    /// </summary>
    /// <param name="id">User id</param>
    /// <param name="cancellationToken">Cancellation token</param>
    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> GetUserById([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        var res = await userService.GetByIdAsync(id, cancellationToken);
        return Ok(res);
    }

    /// <summary>
    /// Updates user
    /// </summary>
    /// <param name="request">Request to update user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var res = await userService.UpdateAsync(request, cancellationToken);
        return Ok(res);
    }

    /// <summary>
    /// Physically deletes user
    /// </summary>
    /// <param name="id">User id</param>
    /// <param name="cancellationToken">Cancellation token</param>
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        await userService.DeleteAsync(id, cancellationToken);
        return Ok();
    }
}