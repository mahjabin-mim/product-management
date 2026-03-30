using Microsoft.Extensions.Caching.Memory;

namespace ProductValidation.Caching
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetOrSetAsync<T>(
            string key,
            Func<Task<T>> getData,
            TimeSpan? absoluteExpireTime = null)
        {
            if (_cache.TryGetValue(key, out T? cachedValue) && cachedValue is not null)
            {
                return cachedValue;
            }

            var value = await getData();

            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromMinutes(5),
                SlidingExpiration = TimeSpan.FromMinutes(2)
            };

            _cache.Set(key, value, options);

            return value;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}