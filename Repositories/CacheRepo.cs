using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace Ecommerce.Repositories
{
    public class CacheRepo(IDistributedCache _cache) : ICache
    {
        private readonly IDistributedCache cache = _cache;

        public async Task<T> GetFromCacheAsync<T>(string key)
        {
            var cachedData = await cache.GetAsync(key);
            if (cachedData == null)
            {
                return default;
            }

            var decodedData = Encoding.UTF8.GetString(cachedData);
            return JsonConvert.DeserializeObject<T>(decodedData);
        }

        public async Task SetAsync<T>(string key, T values)
        {
            
            var serializedData = JsonConvert.SerializeObject(values);
            var encodedData = Encoding.UTF8.GetBytes(serializedData);
            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTimeOffset.UtcNow.AddSeconds(30)); // Adjust expiration as needed
            await cache.SetAsync(key, encodedData, options);
        }

        



    }
}
