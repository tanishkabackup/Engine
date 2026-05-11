namespace TaskInsightEngine.Domain.Constants
{
    public static class RiskTypes
    {
        public const string Low = "Low";
        public const string Medium = "Medium";
        public const string High = "High";
        public const string Critical = "Critical";
        

        public static int MapRiskWeights(string? status)
        => status switch
        {
            Low => 1,
            Medium => 2,
            High => 3,
            Critical => 4,
            _ => 0
        };

        public static string MapRiskStatus(int weight)
        => weight switch
        {
            1 => Low,
            2 => Medium,
            3 => High,
            4 => Critical,
            0 => "Unknown",
            
        };
    }
}
