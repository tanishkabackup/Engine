namespace TaskInsightEngine.Domain.Entities
{
    public class BriefingItem
    {
        public int TaskId { get; set; }
        public string? Title { get; set; }
        public string? Level { get; set; }
        public int? Score { get; set; }
        public int? Delta { get; set; }
        public string? Movement { get; set; }
        public string? TopReasons { get; set; }
        public int AssigneeId { get; set; }
        public int ProjectId { get; set; }
    }
}
