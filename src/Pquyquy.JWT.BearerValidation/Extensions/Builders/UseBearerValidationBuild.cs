using Microsoft.AspNetCore.Builder;
using Pquyquy.JWT.BearerValidation.Middlewares;

namespace Pquyquy.JWT.BearerValidation.Extensions.Builders;

public static class UseBearerValidationBuild
{
    public static void UseBearerValidationMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<JWTUserNameMiddleware>();
    }
}
