namespace TaskInsightEngine.Application.Dtos.Auth
{
    public class UserSession
    {
        public string UserId { get; set; }
        public string SessionId { get; set; }
        public string RefreshToken { get; set; }
    }
}
