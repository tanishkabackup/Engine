namespace TaskInsightEngine.Application.Dtos.Member
{
    public class CreateUserRequest
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
    }
}
