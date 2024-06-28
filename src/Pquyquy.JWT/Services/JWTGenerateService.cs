using Microsoft.IdentityModel.Tokens;
using Pquyquy.JWT.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pquyquy.JWT.Services;

public static class JWTGenerateService
{
    public static string JWTGenerate(JWTSetting jwtSettings, ClaimsIdentity claimsIdentity)
    {
        // appsetting for Token JWT
        var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(jwtSettings.SecretKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        // create token to the user
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
            audience: jwtSettings.AudienceToken,
            issuer: jwtSettings.IssuerToken,
            subject: claimsIdentity,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(jwtSettings.ExpireMinutes),
            signingCredentials: signingCredentials
            );
        var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
        return jwtTokenString;
    }
}
