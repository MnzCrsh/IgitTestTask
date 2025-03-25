using Igit.Abstractions.Contracts;
using Igit.Abstractions.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Igit.Api.Controllers;

/// <summary>
/// Authentication controller
/// </summary>
[ApiController]
[Route("api")]
public class AuthController(IAuthenticationService authService) : ControllerBase
{
    /// <summary>
    /// Authenticates user by his credentials
    /// </summary>
    /// <param name="request">Request to login</param>
    [HttpPost("authenticate")]
    [AllowAnonymous]
    public async Task<IActionResult> CreateJwtBearerToken([FromBody] LoginRequest request)
    {
        var res = await authService.AuthenticateUserAsync(request);
        return Ok(res);
    }
}