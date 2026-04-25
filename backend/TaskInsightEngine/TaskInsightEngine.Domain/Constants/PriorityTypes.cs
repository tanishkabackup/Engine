namespace TaskInsightEngine.Domain.Enums
{
    public static class PriorityTypes
    {
        public const string Low = "Low";
        public const string Medium = "Medium";
        public const string High = "High";

        public static string MapPriority(int id)
        {
            return id switch
            {
                1 => Low,
                2 => Medium,
                3 => High,
                _ => "Unknown"
            };
        }
    };
}

