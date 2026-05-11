namespace TaskInsightEngine.Application.Dtos.Project
{
    public class AddProjectMemberRequest
    {
        public List<int> UserIds { get; set; } = [];
        public int ProjectId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
