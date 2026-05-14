namespace TaskInsightEngine.Domain.Constants
{
    public static class CacheKeys
    {
        public static string RefreshToken(string? sessionId) => $"auth:refresh-tokens:{sessionId}";

        public const string SessionId = "SessionId";

        public const string FullName = "FullName";

        public static string DailyRiskJobId(string? email) => $"daily-risk-snapshot:{email}";
    }
}
