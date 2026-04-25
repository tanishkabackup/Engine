namespace TaskInsightEngine.Application.Interfaces.Cache
{
    public interface ICacheService
    {
        Task SetAsync<T>(string key, T value, TimeSpan? expiry = null);
        Task<T?> GetAsync<T>(string key);
        Task<bool> RemoveAsync(string key);
        Task<bool> ExistsAsync(string key);
    }
}
