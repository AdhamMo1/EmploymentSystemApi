using EmploymentSystemApplication.ServicesContract.Caching;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Concurrent;
using System.Text.Json;

namespace EmploymentSystemInfrastructure.Caching
{
    public class CachingService : ICachingService
    {
        private readonly IDistributedCache _distributedCache;
        private static readonly ConcurrentDictionary<string, bool> CacheKeys = new();  
        public CachingService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T?> GetAsync<T>(string key) where T : class
        {
            string? cachedValue = await _distributedCache.GetStringAsync(key);

            if (cachedValue == null)
            {
                return null;
            }

            T? value = JsonSerializer.Deserialize<T>(cachedValue);
            return value;
        }

        public async Task RemoveAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);
            CacheKeys.TryRemove(key, out bool _);
        }

        public async Task RemoveByPrefixAsync(string prefixKey)
        {
            IEnumerable<Task> tasks = CacheKeys.Keys.Where(x => x.StartsWith(prefixKey)).Select(x => RemoveAsync(x));
            await Task.WhenAll(tasks);
        }

        public async Task SetAsync<T>(string key, T value) where T : class
        {
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6)
            };

            string cachValue = JsonSerializer.Serialize(value);
            await _distributedCache.SetStringAsync(key, cachValue, options);
            CacheKeys.TryAdd(key, false);
        }
    }
}
