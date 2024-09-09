﻿using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;
using System.Text;
using System.Text.Json.Serialization;

namespace Ecommerce.Repositories
{
    public class CacheRepo : ICache
    {


        private readonly IDatabase database;

        public async Task<T?> GetFromCacheAsync<T>(string key)
        {
            if (database == null)
            {
                // Handle null database (log error, throw exception, etc.)
                return default;
            }

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
            await database.KeyDeleteAsync(key);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            var serializedData = JsonConvert.SerializeObject(value, new JsonSerializerSettings
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
