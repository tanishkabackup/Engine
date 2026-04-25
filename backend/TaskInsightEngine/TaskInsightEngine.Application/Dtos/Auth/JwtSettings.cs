namespace TaskInsightEngine.Application.Dtos.Auth
{
    public class JwtSettings
    {
        public const string Section = "Jwt";
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string SecretKey { get; set; }
        public int ExpiryTime { get; set; }
    }
}
