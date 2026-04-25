using TaskInsightEngine.Application.Dtos.Type;
using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Application.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<Role?> GetRoleTypesAsync(GetRoleTypeDto role);
    }
}
