﻿namespace Ecommerce.Services
{
    public interface ICache
    {
        public Task<T?> GetFromCacheAsync<T>(string key);

        public Task SetAsync<T>(string key, T values, TimeSpan expiration);

        public Task RemoveCaching(string key);
    }
}
