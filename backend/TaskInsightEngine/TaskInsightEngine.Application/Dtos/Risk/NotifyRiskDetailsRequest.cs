using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Application.Dtos.Risk
{
    public class NotifyRiskDetailsRequest
    {
       public string Email { get; set; }
        public BriefingSnapshot? BriefingDetails { get; set; } 
    }
}
