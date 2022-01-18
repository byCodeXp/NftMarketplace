using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain;
using Microsoft.IdentityModel.Tokens;

namespace Web.Helpers;

/// <summary>
/// Auxiliary class service. Responsible for generating the jwt token
/// </summary>
public class JwtHelper
{
    private const string SecurityAlgorithm = SecurityAlgorithms.HmacSha512Signature;

    private readonly SigningCredentials _signingCredentials;

    public JwtHelper(SymmetricSecurityKey securityKey)
    {
        _signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithm);
    }

    private static readonly JwtSecurityTokenHandler JwtTokenHandler = new();

    /// <param name="userId"></param>
    /// <param name="userRoles"></param>
    /// <param name="duration">Token lifetime</param>
    /// <returns>Returns jwt token as string</returns>
    public string GenerateToken(string userId, string userRoles, TimeSpan duration)
    {
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(Env.Identity.Claims.Id, userId, ClaimValueTypes.String),
                new(Env.Identity.Claims.Roles, userRoles, ClaimValueTypes.String)
            }),
            Expires = DateTime.UtcNow.Add(duration),
            SigningCredentials = _signingCredentials
        };

        var token = JwtTokenHandler.CreateToken(descriptor);

        return JwtTokenHandler.WriteToken(token);
    }
}