using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TaskInsightEngine.Application.Dtos.Auth;
using TaskInsightEngine.Application.Dtos.Response;
using TaskInsightEngine.Application.Interfaces.Cache;
using TaskInsightEngine.Application.Interfaces.Services;
using TaskInsightEngine.Domain.Constants;
using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwt;
        private readonly RedisSettings _redisSettings;
        private readonly ICacheService _redisService;
        private readonly IAuthService _auth;
        private readonly ILogger<TokenService> _logger;


        public TokenService(IOptions<JwtSettings> options, ICacheService redisService, IOptions<RedisSettings> redisOptions, IAuthService auth, ILogger<TokenService> logger)
        {
            _jwt = options.Value;
            _redisService = redisService;
            _redisSettings = redisOptions.Value;
            _logger = logger;
            _auth = auth;
        }

        public string GenerateRefreshToken(string token)
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            var baseString = Convert.ToBase64String(randomNumber);
            return $"{token}.{baseString}";
        }

        public async Task<TokenResponse> GenerateToken(User user)
        {
            var sessionId = Guid.NewGuid().ToString();
            var claims = new List<Claim>
            {
                new (ClaimTypes.Email,user.Email),
                new (ClaimTypes.Role,user.RoleId.ToString()),
                new (CacheKeys.FullName,user.FullName),
                new (CacheKeys.SessionId,sessionId)
            };

            var keys = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SecretKey));
            var credentials = new SigningCredentials(keys, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken
            (
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.ExpiryTime),
                signingCredentials: credentials
            );

            return new TokenResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor),
                ExpiresAt = tokenDescriptor.ValidTo,
                SessionId = sessionId
            };

        }

        public string GetSessionId(string? token)
        {
            var dotIndex = token.IndexOf('.');
            if (dotIndex <= 0) return null;
            return token[..dotIndex];
        }

        public async Task<TokenResponse> SaveSessionAsync(User user)
        {
            _logger.LogInformation("The process for {Method} has started", nameof(SaveSessionAsync));
            try
            {
                // generate new pair of tokens
                var tokenResponse = await GenerateToken(user);
                var refreshToken = GenerateRefreshToken(tokenResponse.SessionId);

                var session = new UserSession
                {
                    SessionId = tokenResponse.SessionId,
                    RefreshToken = refreshToken,
                    UserId = user.UserId.ToString(),
                };

                var sessionKey = CacheKeys.RefreshToken(session.SessionId);

               // save to redis
                await _redisService.SetAsync(
                    sessionKey,
                    session,
                    TimeSpan.FromDays(_redisSettings.ExpiryTime));

              
                var authTokens = new AuthTokens
                {
                    AccessToken = tokenResponse.AccessToken,
                    RefreshToken = refreshToken,
                    SessionId = tokenResponse.SessionId
                };

                // set cookies
                _auth.SetAuthSession(authTokens);

                _logger.LogInformation("Session saved successfully for key: {Key}", sessionKey);

                return new TokenResponse
                {
                    AccessToken = tokenResponse.AccessToken,
                    RefreshToken= refreshToken,
                    ExpiresAt = tokenResponse.ExpiresAt
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving session");
                throw;
            }
        }
    }
}
