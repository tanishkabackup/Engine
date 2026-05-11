namespace TaskInsightEngine.Application.Dtos.TaskImpediments
{
    public class TaskImpedimentDetail
    {
        public int TaskImpedimentId { get; set; }
        public string? Title { get; set; }
        public string? CreatedBy { get; set; }
        public string? RiskDescription { get; set; }
        public string? RiskStatus { get; set; }
        public string? RiskName { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ResolvedBy { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public bool IsResolved { get; set; }
    }
}
