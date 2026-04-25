namespace TaskInsightEngine.Application.Dtos.Auth
{
    public class RefreshTokenRequest : UserSession
    {
        public string AccessToken { get; set; }
    }
}
