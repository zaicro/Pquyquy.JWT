using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Pquyquy.JWT.Settings;
using System.Security.Claims;
using System.Text;

namespace Pquyquy.JWT.Services;

public static class JWTValidationService
{
    public static TokenValidationParameters CreateTokenValidationParameters(JWTSetting jwtSettings)
    {
        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            LifetimeValidator = LifetimeValidator,
            ValidIssuer = jwtSettings.IssuerToken,
            ValidAudience = jwtSettings.AudienceToken,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(jwtSettings.SecretKey))
        };
    }

    public static JwtBearerEvents CreateJwtBearerEvents() {
        return new JwtBearerEvents();
        //{
        //    OnTokenValidated = c =>
        //    {
        //        var claimsIdentity = c.Principal.Identity as ClaimsIdentity;
        //        return Task.CompletedTask;
        //    },
        //    OnAuthenticationFailed = c =>
        //    {
        //        c.NoResult();
        //        c.Response.StatusCode = 500;
        //        c.Response.ContentType = "text/plain";
        //        return c.Response.WriteAsync($"JWT {c.Exception}");
        //    },
        //    OnChallenge = c =>
        //    {
        //        c.HandleResponse();
        //        c.Response.StatusCode = 401;
        //        c.Response.ContentType = "text/plain";
        //        return c.Response.WriteAsync("JWT Unauthorized");
        //    },
        //    OnForbidden = c =>
        //    {
        //        c.Response.StatusCode = 400;
        //        c.Response.ContentType = "text/plain";
        //        return c.Response.WriteAsync("JWT Bad Request");
        //    }
        //};
    }

    private static bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
    {
        return expires != null && DateTime.UtcNow < expires;
    }
}