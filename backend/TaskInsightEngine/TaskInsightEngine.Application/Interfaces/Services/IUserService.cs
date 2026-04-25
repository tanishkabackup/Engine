using TaskInsightEngine.Application.Dtos.Auth;
using TaskInsightEngine.Application.Dtos.Member;

namespace TaskInsightEngine.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> RegisterUserAsync(CreateUserRequest request);
        Task<GetAllUsersResponse> GetAllUsersAsync();
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest request);
        Task<LogoutResponse> LogoutAsync(LogoutRequest request);
    }
}
