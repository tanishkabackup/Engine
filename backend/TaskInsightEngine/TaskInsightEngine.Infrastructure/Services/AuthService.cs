using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using TaskInsightEngine.Application.Dtos.Auth;
using TaskInsightEngine.Application.Interfaces.Services;

namespace TaskInsightEngine.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _ctx;
        private readonly AuthCookieSettings _options;

       
        public AuthService(IHttpContextAccessor ctx ,IOptions<AuthCookieSettings> options)
        {
            _ctx = ctx;
            _options = options.Value;
        }

        private HttpResponse response => _ctx.HttpContext?.Response ?? throw new InvalidOperationException("No active HttpContext");
        public void ClearAuthSession()
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, 
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(-1)
            };
            response.Cookies.Delete(_options.AccessCookieName, new CookieOptions { Path = "/" });
            response.Cookies.Delete(_options.RefreshCookieName, new CookieOptions { Path = _options.RefreshPath });
           
        }

        public string? GetRefreshToken()
        {
            return _ctx.HttpContext?.Request.Cookies[_options.RefreshCookieName];
        }

        public void SetAuthSession(AuthTokens session)
        {
            var accessOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = _options.SameSiteMode,
                Expires = DateTimeOffset.UtcNow.AddMinutes(_options.AccessTokenMinutes),
                IsEssential = true
            };

            var refreshOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = _options.SameSiteMode,
                Expires = DateTimeOffset.UtcNow.AddDays(_options.RefreshTokenDays),
                IsEssential = true
            };

            SetCookie(_options.AccessCookieName, session.AccessToken, accessOptions);
            SetCookie(_options.RefreshCookieName, session.RefreshToken, refreshOptions);
        }


        public void SetCookie(string name, string value, CookieOptions options)
        {
            response.Cookies.Append(name, value ?? string.Empty, options);
        }
    }
}
