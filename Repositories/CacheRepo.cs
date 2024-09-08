using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;
using System.Text;
using System.Text.Json.Serialization;

namespace Ecommerce.Repositories
{
    public class CacheRepo(IDatabase _database) : ICache
    {


        private readonly IDatabase database = _database;

        public async Task<T?> GetFromCacheAsync<T>(string key)
        {
            var cachedData = await database.StringGetAsync(key);
            if (cachedData.IsNullOrEmpty)
            {
                return default;
            }
            var decodedData = cachedData.ToString();
            return JsonConvert.DeserializeObject<T>(decodedData, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });
        }
        public async Task RemoveCaching(string key)
        {
            var success = await _database.KeyDeleteAsync(key);

            if (!success)
            {
                throw new Exception("Failed to remove key from Redis.");
            }
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            // Serialize the object to JSON
            var serializedData = JsonConvert.SerializeObject(value, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });

            // Store the JSON string in Redis with expiration
            var success = await _database.StringSetAsync(key, serializedData, expiration);

            if (!success)
            {
                throw new Exception("Failed to set value in Redis.");
            }
        }





    }
}
