namespace TaskInsightEngine.Domain.Constants
{
    public static class RiskMovement
    {
       public const string Stable = "Stable";
       public const string New = "New";
       public const string Escalated = "Escalated";
       public const string Improved = "Improved";
       public const string Silent = "Silent";
       public const string Critical = "Critical";
       public const string Healthy = "Healthy";
       public const string HealthyMessage = "Task is on track";

        public static int GetMovementId(string movement)
       {
            return movement switch
            {
                New => 1,
                Stable => 2,
                Improved => 3,
                Escalated => 4,
                Critical => 5,
                Silent => 0, 
                _ => 0  
            };
       }

        public static string GetMovementName(int id)
        {
            return id switch
            {
                1 => New,
                2 => Stable,
                3 => Improved,
                4 => Escalated,
                5 => Critical,
                0 => Silent,
                _ => Silent 
            };
        }
    }
}
