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
        try
        {
            var userName = context.User.Identity?.Name;
            if (!string.IsNullOrEmpty(userName))
            {
                context.Items["JWTUserName"] = userName;
            }

            await _requestDelegate(context);
        }
        catch (Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var errors = new List<string>()
            {
                { $"innerException: {ex.InnerException}" },
                { $"stackTrace: {ex.StackTrace}" }
            };

            var responseModel = new Dictionary<string, object>()
            {
                { "succeeded", false },
                { "message", ex.Message },
                { "errors", errors }
            };
            var result = JsonConvert.SerializeObject(responseModel);
            await response.WriteAsync(result);
        }
    }
}
