using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Pquyquy.JWT.Services;
using Pquyquy.JWT.Settings;

namespace Pquyquy.JWT.BearerValidation
{
    public static class DependencyInjection
    {
        public static void AddJWTServices(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<JWTSetting>(configuration.GetSection("JWTSettings"));
            var serviceProvider = service.BuildServiceProvider();
            var jwtSettings = serviceProvider.GetRequiredService<IOptions<JWTSetting>>().Value;

            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = JWTValidationService.CreateTokenValidationParameters(jwtSettings);
                o.Events = JWTValidationService.CreateJwtBearerEvents();
            });
        }
    }
}
