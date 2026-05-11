using TaskInsightEngine.Application.Dtos.Response;
using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Application.Interfaces.Services
{
    public interface ITokenService
    {
        Task<TokenResponse> GenerateToken(User user);
        Task<TokenResponse> SaveSessionAsync(User user);
        string GenerateRefreshToken(string token);
        string? GetSessionId(string? token);
    }
}
