namespace TaskInsightEngine.Domain.Entities
{
    public class RiskSnapshot
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string? Title { get; set; }
        public int ProjectId { get; set; }
        public DateTime Date { get; set; }
        public int? PrevScore { get; set; }
        public string? PrevLevel { get; set; }
        public int MovementId { get; set; }
        public int? Delta { get; set; }
        public int? CurrentScore { get; set; }
        public string? CurrentLevel { get; set; }
        public string? TopReasons { get; set; }
        public int AssigneeId { get; set; }
        public TaskItem TaskItem { get; set; }
    }
}
