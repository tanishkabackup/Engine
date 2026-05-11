using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Application.Dtos.Risk
{
    public class OpenTaskDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime DueDate { get; set; }
        public List<TaskImpediment> TaskImpediments { get; set; } = [];
        public List<DailyTaskUpdateStatus?> DailyTaskUpdateStatus { get; set; } = [];
        public int AssigneeId { get; set; }
    }
}
