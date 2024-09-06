using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace Ecommerce.Repositories
{
    public class CacheRepo(IDistributedCache _cache) : ICache
    {
        private readonly IDistributedCache cache = _cache;

        public async Task<T?> GetFromCacheAsync<T>(string key)
        {
            var cachedData = await cache.GetAsync(key);
            if (cachedData == null)
            {
                return default;
            }
            var decodedData = Encoding.UTF8.GetString(cachedData);
            return JsonConvert.DeserializeObject<T>(decodedData, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });
        }

        public async Task RemoveCaching(string key)
        {
            await cache.RemoveAsync(key);
        }

        public async Task SetAsync<T>(string key, T values)
        {
            var serializedData = JsonConvert.SerializeObject(values, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });
            var encodedData = Encoding.UTF8.GetBytes(serializedData);

            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTimeOffset.UtcNow.AddSeconds(30));

            // Convert the byte array back to a string
            var encodedString = Encoding.UTF8.GetString(encodedData);

            // Set the string data asynchronously
            await cache.SetStringAsync(key, encodedString, options);
        }





    }
}
