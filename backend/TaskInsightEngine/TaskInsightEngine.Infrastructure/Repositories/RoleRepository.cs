using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskInsightEngine.Application.Dtos.Type;
using TaskInsightEngine.Application.Interfaces.Repositories;
using TaskInsightEngine.Domain.Entities;
using TaskInsightEngine.Infrastructure.Persistence;

namespace TaskInsightEngine.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private ILogger<RoleRepository> _logger;
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context, ILogger<RoleRepository> logger)
        {
            _context = context;
            _logger = logger;

        }
        public async Task<Role?> GetRoleTypesAsync(GetRoleTypeDto role)
        {
            _logger.LogInformation("Database operations for {Method} started ", nameof(GetRoleTypesAsync));
            try
            {
               return await _context.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.Name == role.Type);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error for {Method}", nameof(GetRoleTypesAsync));
                throw;
            }
        }
    }
}
