using FightTracker.Application.CachingServices;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Infrastructure.Cacheservices
{
    public class CachingService : ICachingService
    {
        private readonly IMemoryCache memoryCache;

        public CachingService(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }
        public Task DeleteAsync(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                memoryCache.Remove(key);
            }
            return Task.CompletedTask;
        }

        public Task<T?> GetAsync<T>(string key)
        {
            memoryCache.TryGetValue(key, out T? value);
            return Task.FromResult(value);
        }

        public Task SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                memoryCache.Set(key, value, expiration);
            }
            return Task.CompletedTask;
        }
    }
}
