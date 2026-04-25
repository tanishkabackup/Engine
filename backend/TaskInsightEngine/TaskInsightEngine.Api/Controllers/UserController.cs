using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TaskInsightEngine.Application.Dtos.Auth;
using TaskInsightEngine.Application.Dtos.Member;
using TaskInsightEngine.Application.Interfaces.Services;
using TaskInsightEngine.Domain.Constants;

namespace TaskInsightEngine.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AuthCookieSettings _authCookieSettings;
        public UserController(IUserService userService, IOptions<AuthCookieSettings> options)
        {
            _userService = userService;
            _authCookieSettings = options.Value;
        }

        [HttpPost]
        [ApiExplorerSettings(GroupName = "v1")]
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterUser(CreateUserRequest request)
        {
            var response = await _userService.RegisterUserAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        [ApiExplorerSettings(GroupName = "v1")]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _userService.GetAllUsersAsync();
            return Ok(response);
        }

        [HttpPost]
        [ApiExplorerSettings(GroupName = "v1")]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var response = await _userService.LoginAsync(request);
            return Ok(response);
        }

        
        [HttpPost]
        [ApiExplorerSettings(GroupName = "v1")]
        [Route("Refresh")]
        public async Task<IActionResult> RefreshToken()
        {
          
            var refreshTokenRequest = new RefreshTokenRequest
            {
                RefreshToken = Request.Cookies[_authCookieSettings.RefreshCookieName],
            };
            var response = await _userService.RefreshTokenAsync(refreshTokenRequest);
            return Ok(response);
        }


        [HttpPost]
        [ApiExplorerSettings(GroupName = "v1")]
        [Authorize]
        [Route("Logout")]
        public async Task<IActionResult>Logout()
        {

            var logoutRequest = new LogoutRequest
            {
                SessionId = User.FindFirst(CacheKeys.SessionId)?.Value
            };
            var response = await _userService.LogoutAsync(logoutRequest);
            return Ok(response);
        }
    }
}
