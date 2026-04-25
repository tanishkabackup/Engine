using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskInsightEngine.Application.Dtos.Auth;

namespace TaskInsightEngine.Api.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var jwt = config.GetSection(JwtSettings.Section).Get<JwtSettings>();
            var authCookie = config.GetSection(AuthCookieSettings.Section).Get<AuthCookieSettings>();

            if (jwt == null)
            {
                throw new Exception("JWT configuration is missing");
            }

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwt.Issuer,

                        ValidateAudience = true,
                        ValidAudience = jwt.Audience,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwt.SecretKey)
                        ),

                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        { 
                            context.Token = context.Request.Cookies[authCookie.AccessCookieName];
                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }
    }
}
