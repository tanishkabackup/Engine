using Microsoft.EntityFrameworkCore;
using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<TaskAssignment> TaskAssignments { get; set; }
        public DbSet<DailyTaskUpdateStatus> DailyTaskUpdateStatuses {get ; set;}
        public DbSet<TaskImpediment> TaskImpediments { get; set; }
        public DbSet<TaskImpedimentComment> TaskImpedimentComments { get; set; }
        public DbSet<Risk> Risks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
