using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskInsightEngine.Application.Dtos.Type;
using TaskInsightEngine.Application.Interfaces.Repositories;
using TaskInsightEngine.Domain.Entities;
using TaskInsightEngine.Infrastructure.Persistence;

namespace TaskInsightEngine.Infrastructure.Repositories
{
    public class PriorityRepository : IPriorityRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PriorityRepository> _logger;

        public PriorityRepository(ApplicationDbContext context, ILogger<PriorityRepository> logger)
        {
            _context = context;
            _logger = logger;

        }
        public async Task<Priority?> GetPriorityTypesAsync(GetPriorityTypeDto priority)
        {
            _logger.LogInformation("Database operations for {Method} started ", nameof(GetPriorityTypesAsync));
            try
            {
                return await _context.Priorities.AsNoTracking().FirstOrDefaultAsync(p => p.Type == priority.Type || p.PriorityId == priority.Id);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error for {Method}", nameof(GetPriorityTypesAsync));
                throw;
            }
        }
    }
}
