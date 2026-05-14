namespace TaskInsightEngine.Application.Dtos.Task
{
    public class TaskHistoryDto
    {
        public int TaskId { get; set; }
        public string? Title { get; set; }
        public DateTime Date { get; set; }
        public string? RiskChange { get; set; }
        public string? Impact { get; set; }
        public string? Owner { get; set; }
        public string? KeyInsight { get; set; }
    }
}
