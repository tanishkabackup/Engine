using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Text.Json;
using TaskInsightEngine.Application.Interfaces.Cache;

namespace TaskInsightEngine.Infrastructure.Caching
{
    public class RedisService : ICacheService
    {
        private readonly IDatabase _database;
        private readonly ILogger<RedisService> _logger;

        public RedisService(IConnectionMultiplexer redis, ILogger<RedisService> logger)
        {
            _database = redis.GetDatabase();
            _logger = logger;

        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            try
            {
                var serializedValue = JsonSerializer.Serialize(value);
                await _database.StringSetAsync(key, serializedValue, expiry);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error setting cache key: {key}",key);
                throw;
            }
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            try
            {
                var value = await _database.StringGetAsync(key);
                if(value.IsNullOrEmpty)
                {
                    return default;
                }
                return JsonSerializer.Deserialize<T>(value!);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error fetching cache key: {key}", key);
                return default;
            }
        }

        public async Task<bool> RemoveAsync(string key)
        {
           return await _database.KeyDeleteAsync(key);
        }

        public async Task<bool> ExistsAsync(string key)
        {
          return await _database.KeyExistsAsync(key);
        }
    }
}
