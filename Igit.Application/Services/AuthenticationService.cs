using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Igit.Abstractions.Contracts;
using Igit.Abstractions.Models.Requests;
using Igit.Entities.Entities;
using Igit.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Igit.Application.Services;

/// <inheritdoc/>
public class AuthenticationService(CoreDbContext context) : IAuthenticationService
{
    /// <inheritdoc/>
    public async Task<string> AuthenticateUserAsync(LoginRequest request)
    {
        var user = await context.Set<User>()
                       .AsNoTracking()
                       .Include(x => x.Role)
                       .FirstOrDefaultAsync(x => x.Email == request.Email && x.Id == request.UserId)
                   ?? throw new UnauthorizedAccessException();

        return GenerateJwtToken(user);
    }

    private static string GenerateJwtToken(User user)
    {
        var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")
                                          ?? throw new InvalidOperationException("JWT_KEY environment variable is missing"));

        List<Claim> claims =
        [
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.FullName),
            new(ClaimTypes.Role, user.Role.Name)
        ];

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}