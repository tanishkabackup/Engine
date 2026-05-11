namespace TaskInsightEngine.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Role? Role { get; set; }
        public ICollection<ProjectMember> Members { get; set; } = [];
    }
}
