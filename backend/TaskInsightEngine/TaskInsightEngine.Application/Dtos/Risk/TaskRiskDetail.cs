using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Application.Dtos.Risk
{
    public class TaskRiskDetail
    {
        public int TaskItemId { get; set; }
        public int ProjectId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int AssigneeId { get; set; }
        public DateTime DueDate { get; set; }
        public List<DailyTaskUpdateStatus> DailyTaskUpdateStatuses { get; set; } = [];
        public List<TaskImpediment> TaskImpediments { get; set; } = [];

    }
}
