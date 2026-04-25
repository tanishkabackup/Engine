using TaskInsightEngine.Application.Dtos.Auth;

namespace TaskInsightEngine.Application.Interfaces.Services
{
    public interface IAuthService
    {
        void SetAuthSession(AuthTokens session);
        void ClearAuthSession();
        string? GetRefreshToken();
    }
}
