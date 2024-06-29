using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Pquyquy.JWT.BearerValidation.Middlewares;

public class JWTUserNameMiddleware
{
    private readonly RequestDelegate _requestDelegate;

    public JWTUserNameMiddleware(RequestDelegate requestDelegate)
    {
        _requestDelegate = requestDelegate ?? throw new ArgumentNullException(nameof(requestDelegate));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var userName = context.User.Identity?.Name;
        if (!string.IsNullOrEmpty(userName))
        {
            context.Items["JWTUserName"] = userName;
        }

        await _requestDelegate(context);
    }
}
