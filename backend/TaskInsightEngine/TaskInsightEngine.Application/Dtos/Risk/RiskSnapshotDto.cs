namespace TaskInsightEngine.Application.Dtos.Risk
{
    public class RiskSnapshotDto
    {
        public int TaskId { get; set; }
        public int ProjectId { get; set; }
        public string? Title { get; set; }
        public string? AssigneeName { get; set; }
        public string? AssigneeRole { get; set; }
        public DateTime Date { get; set; }
        public int? PrevScore { get; set; }
        public int? CurrentScore { get; set; }
        public string? PrevLevel { get; set; }
        public string? CurrentLevel { get; set; }
        public int? Delta { get; set; }
        public string? TopReasons { get; set; }
        public int MovementId { get; set; }
        public int AssigneeId { get; set; }
    }
}
