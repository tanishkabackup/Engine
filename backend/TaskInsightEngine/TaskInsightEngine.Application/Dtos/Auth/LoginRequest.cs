using System.ComponentModel.DataAnnotations;

namespace TaskInsightEngine.Application.Dtos.Auth
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
