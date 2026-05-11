using Microsoft.AspNetCore.Http;

namespace TaskInsightEngine.Application.Dtos.Auth
{
    public class AuthCookieSettings
    {
        public const string Section = "AuthCookies";
        public SameSiteMode SameSiteMode { get; set; } = SameSiteMode.None;
        public int AccessTokenMinutes { get; set; }
        public int RefreshTokenDays { get; set; }
        public string AccessCookieName { get; set; }
        public string RefreshCookieName { get; set; }

        public string RefreshPath { get; set; }
    }
}
