namespace TaskInsightEngine.Application.Dtos.Auth
{
    public class RedisSettings
    {
        public const string Section = "Redis";
        public string ConnectionStrings { get; set; }
        public int ExpiryTime { get; set; }
    }
}
