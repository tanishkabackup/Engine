using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskInsightEngine.Application.Dtos.Type;
using TaskInsightEngine.Application.Interfaces.Repositories;
using TaskInsightEngine.Domain.Entities;
using TaskInsightEngine.Infrastructure.Persistence;

namespace TaskInsightEngine.Infrastructure.Repositories
{
    public class RiskRepository :IRiskRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RiskRepository> _logger;

        public RiskRepository(ApplicationDbContext context, ILogger<RiskRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddRiskDetailsAsync(Risk risk)
        {
            _logger.LogInformation("Database operations for {Method} started ", nameof(AddRiskDetailsAsync));
            try
            {
               _context.Risks.Add(risk);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Database operations for {Method} is completed ", nameof(AddRiskDetailsAsync));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error for {Method}", nameof(AddRiskDetailsAsync));
                throw;
            }
        }
    }
}
