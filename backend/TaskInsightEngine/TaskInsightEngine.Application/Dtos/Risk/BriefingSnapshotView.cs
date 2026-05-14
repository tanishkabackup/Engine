using TaskInsightEngine.Application.Dtos.Project;
using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Application.Dtos.Risk
{
    public class BriefingSnapshotView
    {
        public List<BriefingItem> NeedsAttention { get; set; } = [];
        public List<BriefingItem> SilentRisk { get; set; } = [];
        public List<BriefingItem> Recovering { get; set; } = [];
        public List<RiskSnapshotDto> Healthy { get; set; } = [];
        public ProjectTaskHistory? History { get; set; }
    }
}
