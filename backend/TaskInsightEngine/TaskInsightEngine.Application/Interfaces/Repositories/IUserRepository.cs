using TaskInsightEngine.Application.Dtos.Member;
using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Application.Interfaces.Repositories
{
    public interface IUserRepository 
    {
        Task AddNewUserAsync(User user);
        Task<User?> GetUserDetails(GetUserDetailsDto user);
        Task<GetAllUsersResponse> GetAllUsersAsync();
    }
}
