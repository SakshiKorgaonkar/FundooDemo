using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepoLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RepoLayer.Utility
{
    public class Caching<T>
    {
        private readonly ProjectContext projectContext;
        private readonly IDistributedCache cache;

        public Caching(ProjectContext projectContext,IDistributedCache cache)
        {
            this.projectContext = projectContext;
            this.cache = cache;
        }
        public List<T> GetAll<T>(string cacheKey, Func<List<T>> fetchFunc, int cacheDurationInMinutes = 10)
        {
            var cachedData = cache.GetString(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonConvert.DeserializeObject<List<T>>(cachedData);
            }

            var data = fetchFunc();
            cache.SetString(cacheKey, JsonConvert.SerializeObject(data), new DistributedCacheEntryOptions
            {
                 AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(cacheDurationInMinutes)
            });
            return data;
        }
        public T GetById(string cacheKey, Func<T> fetchFunc, int cacheDurationInMinutes = 10)
        {
            var cachedData = cache.GetString(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonConvert.DeserializeObject<T>(cachedData);
            }

            var data = fetchFunc();
            cache.SetString(cacheKey, JsonConvert.SerializeObject(data), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(cacheDurationInMinutes)
            });
            return data;
        }
        public void Update(string cacheKey)
        {
            cache.Remove(cacheKey);
        }
    }
}
