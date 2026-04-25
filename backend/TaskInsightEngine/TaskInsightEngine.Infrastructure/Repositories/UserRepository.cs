using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskInsightEngine.Application.Dtos.Member;
using TaskInsightEngine.Application.Interfaces.Repositories;
using TaskInsightEngine.Domain.Entities;
using TaskInsightEngine.Infrastructure.Persistence;

namespace TaskInsightEngine.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(ApplicationDbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;

        }
        public async Task AddNewUserAsync(User user)
        {
            _logger.LogInformation("Database operations for {Method} started ", nameof(AddNewUserAsync));
            try
            {
               _context.Users.Add(user);
               await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Database operations error for {Method}", nameof(AddNewUserAsync));
                throw;
            }
        }

        public async Task<User?> GetUserDetails(GetUserDetailsDto user)
        {
            _logger.LogInformation("Database operations for {Method} started ", nameof(AddNewUserAsync));
            try
            {
                if (user.UserId != 0)
                {
                    return await _context.Users.AsNoTracking().Include(u=>u.Role).FirstOrDefaultAsync(u => u.UserId == user.UserId);
                }
                else if(!String.IsNullOrEmpty(user.Email))
                {
                    return await _context.Users.AsNoTracking().Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == user.Email);
                }

                return null;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database operations error for {Method}", nameof(AddNewUserAsync));
                throw;
            }
        }

        public async Task<GetAllUsersResponse> GetAllUsersAsync()
        {
            _logger.LogInformation("Database operations for {Method} started ", nameof(GetAllUsersAsync));
            try
            {

                var userList = await _context.Users
                              .Include(u => u.Role)
                              .Select(u => new UserDetail
                              {
                                UserId = u.UserId,
                                UserName = u.FullName,
                                Email = u.Email,
                                Role = u.Role.Name
                              }).AsNoTracking()
                             .ToListAsync();

                return new GetAllUsersResponse
                {
                    Users = userList
                };


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error for {Method}", nameof(GetAllUsersAsync));
                throw;
            }
        }
    }
}
