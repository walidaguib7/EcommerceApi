using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Services;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Ecommerce.Repositories
{
    public class RedisService : ICache
    {
        private readonly IConnectionMultiplexer connection;

        public RedisService(IConnectionMultiplexer _connection)
        {
            connection = _connection;
        }
        public async Task<T?> GetFromCacheAsync<T>(string key)
        {
            var database = connection.GetDatabase();
            if (database == null) throw new Exception("redis database not found");

            var cachedData = await database.StringGetAsync(key);
            if (cachedData.IsNullOrEmpty)
            {
                return default;
            }

            try
            {
                return JsonConvert.DeserializeObject<T>(cachedData, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
            }
            catch (Exception ex)
            {
                // Handle deserialization errors appropriately
                Console.WriteLine($"Error deserializing cached data: {ex.Message}");
                return default;
            }
        }

        public async Task RemoveCaching(string key)
        {
            var database = connection.GetDatabase();
            await database.KeyDeleteAsync(key);
        }

        public async Task SetAsync<T>(string key, T values, TimeSpan expiration)
        {
            var database = connection.GetDatabase();
            var serializedData = JsonConvert.SerializeObject(values, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });

            try
            {
                await database.StringSetAsync(key, serializedData, expiration);
            }
            catch (Exception ex)
            {
                // Handle Redis operations errors appropriately
                Console.WriteLine($"Error setting cache: {ex.Message}");
            }
        }
    }
}