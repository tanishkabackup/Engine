using TaskInsightEngine.Application.Dtos.Type;
using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Application.Interfaces.Repositories
{
    public interface IPriorityRepository
    {
        Task<Priority?> GetPriorityTypesAsync(GetPriorityTypeDto priority);
    }
}
