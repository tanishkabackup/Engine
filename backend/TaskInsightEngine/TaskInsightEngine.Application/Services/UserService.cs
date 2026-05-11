using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TaskInsightEngine.Application.Dtos.Auth;
using TaskInsightEngine.Application.Dtos.Member;
using TaskInsightEngine.Application.Dtos.Type;
using TaskInsightEngine.Application.Interfaces.Cache;
using TaskInsightEngine.Application.Interfaces.Repositories;
using TaskInsightEngine.Application.Interfaces.Services;
using TaskInsightEngine.Domain.Constants;
using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Application.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ITokenService _token;
        private readonly IAuthService _auth;
        private readonly ICacheService _cacheService;


        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, ITokenService tokenService, ICacheService cacheService, IAuthService auth, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _token = tokenService;
            _cacheService = cacheService;
            _logger = logger;
            _auth = auth;
        }
        public async Task<CreateUserResponse> RegisterUserAsync(CreateUserRequest request)
        {
            _logger.LogInformation("The process for {Method} has started - {FullName}", nameof(RegisterUserAsync), request.Fullname);
            try
            {
                var role = new GetRoleTypeDto
                {
                    Type = request.Role
                };

                var userRole = await _roleRepository.GetRoleTypesAsync(role);

                if (userRole is null)
                {
                    return new CreateUserResponse
                    {
                        IsSuccess = false,
                        Message = "Invalid Role"
                    };
                }

                var user = new User
                {
                    FullName = request.Fullname,
                    Email = request.Email,
                    Password = request.Password,
                    RoleId = userRole.RoleId

                };

                await _userRepository.AddNewUserAsync(user);
                _logger.LogInformation("The process for {Method} is sucessfully completed", nameof(RegisterUserAsync));

                return new CreateUserResponse
                {
                    IsSuccess = true,
                    Message = "User Registered Successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the {Method} process.", nameof(RegisterUserAsync));
                throw;
            }
        }

        public async Task<GetAllUsersResponse> GetAllUsersAsync()
        {
            _logger.LogInformation("The process for {Method} has started", nameof(GetAllUsersAsync));
            try
            {
                var usersList = await _userRepository.GetAllUsersAsync();

                _logger.LogInformation("The process for {Method} is completed", nameof(GetAllUsersAsync));

                return usersList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the {Method} process.", nameof(GetAllUsersAsync));
                throw;
            }
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            _logger.LogInformation("The process for {Method} has started", nameof(LoginAsync));
            try
            {
                var userDetailsRequest = new GetUserDetailsDto
                {
                    Email = request.Email
                };
                var userDetailResponse = await _userRepository.GetUserDetails(userDetailsRequest) ?? throw new UnauthorizedAccessException("Invalid email or password.");
                var user = new User
                {
                    Email = request.Email,
                    RoleId = userDetailResponse.RoleId,
                    UserId = userDetailResponse.UserId,
                    FullName = userDetailResponse.FullName,
                    Role = userDetailResponse.Role
                };


                await _token.SaveSessionAsync(user);

                var userDetails = new UserLoginDto
                {
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = user.Role.Name,
                };

                return new LoginResponse
                {
                    User = userDetails
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the {Method} process.", nameof(LoginAsync));
                throw;
            }
        }

        public async Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest request)
        {
            _logger.LogInformation("The process for {Method} has started", nameof(RefreshTokenAsync));

            try
            {
                var refreshTokenResponse = new RefreshTokenResponse();
                var sessionId = _token.GetSessionId(request.RefreshToken);

                if (!string.IsNullOrEmpty(sessionId))
                {
                    request.SessionId = sessionId;
                }
                else
                {
                    throw new Exception("UserId not valid.");
                }

                string refreshCacheKey = CacheKeys.RefreshToken(sessionId);


                //check session
                bool isTokenActive = await _cacheService.ExistsAsync(refreshCacheKey);

                if (!isTokenActive)
                {
                    _logger.LogError("Token is inactive");
                    throw new Exception("Session expired or logged out.");
                }

                // security check: check if the provided refresh token is valid and can be retrived from redis
                var cachedSessionDetails = await _cacheService.GetAsync<UserSession>(refreshCacheKey);
                if (cachedSessionDetails?.RefreshToken != request.RefreshToken)
                {
                    _logger.LogError("Invalid token");
                    await _cacheService.RemoveAsync(refreshCacheKey);
                    throw new SecurityTokenException("Invalid token. All sessions for this user revoked.");
                }

                // check if user is valid 
                var userDetailsRequest = new GetUserDetailsDto
                {
                    UserId = Convert.ToInt16(cachedSessionDetails.UserId)
                };
                var user = await _userRepository.GetUserDetails(userDetailsRequest);
                if (user is null)
                {
                    throw new Exception("User does not exist");
                }

                _logger.LogInformation("The process for {Method} is completed", nameof(RefreshTokenAsync));

                var response = await _token.SaveSessionAsync(user);

                refreshTokenResponse.AccessToken = response.AccessToken;
                refreshTokenResponse.RefreshToken = response.RefreshToken;
                refreshTokenResponse.ExpiresAt = response.ExpiresAt;

                return refreshTokenResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the {Method} process.", nameof(RefreshTokenAsync));
                throw;
            }
        }

        public async Task<LogoutResponse> LogoutAsync(LogoutRequest request)
        {
            _logger.LogInformation("The process for {Method} has started", nameof(LogoutAsync));
            try
            {
                if (!string.IsNullOrEmpty(request.SessionId))
                {
                    var cacheKey = CacheKeys.RefreshToken(request.SessionId);
                    
                   var isRemoved = await _cacheService.RemoveAsync(cacheKey);

                    if (isRemoved)
                    {
                        _logger.LogDebug("Session {SessionId} successfully evicted from Redis", request.SessionId);
                    }
                    else
                    {
                        _logger.LogError("Logout: SessionId {SessionId} was not found in cache.", request.SessionId);
                    }

                    _auth.ClearAuthSession();

                    _logger.LogInformation("The process for {Method} is completed", nameof(LogoutAsync));

                    return new LogoutResponse
                    {
                        IsSuccess = true,
                        Message = "User logged out successfully",

                    };
                }
                else
                {
                    return new LogoutResponse
                    {
                        IsSuccess = false,
                        Message = "User log out failed",

                    };
                }

               
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the {Method} process.", nameof(LogoutAsync));
                throw;
            }
        }
    }
}
