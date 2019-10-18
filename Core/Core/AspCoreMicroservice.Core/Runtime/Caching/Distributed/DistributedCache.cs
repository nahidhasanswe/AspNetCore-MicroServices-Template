using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreMicroservice.Core.Runtime.Caching.Distributed
{
    /// <summary>
    /// Used to store cache in a distributed cache server.
    /// </summary>
    public class DistributedCache : CacheBase
    {
        private readonly ICacheSerializer _serializer;
        private readonly IDistributedCache cache;

        /// <summary>
        /// Constructor.
        /// </summary>
        public DistributedCache(ILogger logger, string name, IDistributedCache cache, ICacheSerializer serializer)
            : base(logger, name)
        {
            this.cache = cache;
            _serializer = serializer;
        }

        public override object GetOrDefault(string key)
        {
            var objbyte = cache.GetString(key);
            return Deserialize(objbyte);
        }

        public override object[] GetOrDefault(string[] keys)
        {
            if (keys == null || !keys.Any())
                return null;

            var result = new List<object>();  
            Parallel.ForEach(keys, key =>
            {
                var obj = GetOrDefault(key);
                if (obj != null)
                    result.Add(obj); 
            });

            return result.ToArray();
        }

        public override async Task<object> GetOrDefaultAsync(string key)
        {
            var objbyte = await cache.GetStringAsync(key);
            return Deserialize(objbyte);
        }

        public override async Task<object[]> GetOrDefaultAsync(string[] keys)
        {
            if (keys == null || !keys.Any())
                return null;

            var result = new List<object>();
            foreach(var key in keys)
            {
                var obj = await GetOrDefaultAsync(key);
                if (obj != null)
                    result.Add(obj);
            }
         
            return result.ToArray();
        }

        public override void Set(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null)
        {
            Check.NotNull(value, nameof(value));
            cache.SetString(key,
                Serialize(value, GetSerializableType(value)),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = absoluteExpireTime != null 
                        ? DateTimeOffset.UtcNow.Add(absoluteExpireTime.Value) 
                        : slidingExpireTime == null ? DateTimeOffset.UtcNow.Add(DefaultAbsoluteExpireTime) : (DateTimeOffset?) null,
                    SlidingExpiration = slidingExpireTime != null ? slidingExpireTime : DefaultSlidingExpireTime
                });
        }

        public override async Task SetAsync(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null)
        {
            Check.NotNull(value, nameof(value));

            await cache.SetStringAsync(key,
                Serialize(value, GetSerializableType(value)),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = absoluteExpireTime != null
                        ? DateTimeOffset.UtcNow.Add(absoluteExpireTime.Value)
                        : slidingExpireTime == null ? DateTimeOffset.UtcNow.Add(DefaultAbsoluteExpireTime) : (DateTimeOffset?)null,
                    SlidingExpiration = slidingExpireTime != null ? slidingExpireTime : DefaultSlidingExpireTime
                });
        }

        public override void Set(KeyValuePair<string, object>[] pairs, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null)
        {
            if (pairs.Any(p => p.Value == null))
                throw new ArgumentNullException(nameof(pairs), "Can not insert null values to the cache!");

            foreach(var pair in pairs)
            {
                Set(pair.Key, pair.Value, slidingExpireTime, absoluteExpireTime);
            }
        }

        public override async Task SetAsync(KeyValuePair<string, object>[] pairs, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null)
        {
            if (pairs.Any(p => p.Value == null))
                throw new ArgumentNullException(nameof(pairs), "Can not insert null values to the cache!");

            foreach (var pair in pairs)
            {
                await SetAsync(pair.Key, pair.Value, slidingExpireTime, absoluteExpireTime);
            }
        }

        public override void Remove(string key)
        {
            cache.Remove(key);
        }

        public override async Task RemoveAsync(string key)
        {
            await cache.RemoveAsync(key);
        }

        public override void Remove(string[] keys)
        {
            foreach (var key in keys)
                Remove(key);
        }

        public override async Task RemoveAsync(string[] keys)
        {
            foreach (var key in keys)
                await RemoveAsync(key);
        }

        public override void Clear()
        {
            throw new NotSupportedException();
        }

        protected virtual Type GetSerializableType(object value)
        {
            return value.GetType();
        }

        protected virtual string Serialize(object value, Type type)
        {
            return _serializer.Serialize(value, type);
        }

        protected virtual object Deserialize(string objbyte)
        {
            return _serializer.Deserialize(objbyte);
        }           
    }
}
