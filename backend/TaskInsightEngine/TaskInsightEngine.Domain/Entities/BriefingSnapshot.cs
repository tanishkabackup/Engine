using System.ComponentModel.DataAnnotations.Schema;

namespace TaskInsightEngine.Domain.Entities
{
    public class BriefingSnapshot
    {
        public List<BriefingItem> NeedsAttention { get; set; }
        public List<BriefingItem> SlientRisk { get; set; }
        public List<BriefingItem> Recovering { get; set; }
    }
}
