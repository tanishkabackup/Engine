using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Application.Dtos.Risk
{
    public class SaveBriefingDetailsRequest
    {
        public List<BriefingItem> BriefingItems { get; set; } = [];
    }
}
