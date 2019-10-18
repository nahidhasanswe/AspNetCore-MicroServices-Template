// ASP.NET Boilerplate - Web Application Framework https://aspnetboilerplate.com
// Copyright (c) 2013-2017 Volosoft (https://volosoft.com)
// This code is licensed under MIT license (see LICENSE.txt for details)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreMicroservice.Core.Runtime.Caching
{
    /// <summary>
    /// Implements <see cref="ITypedCache{string,TValue}"/> to wrap a <see cref="ICache"/>.
    /// </summary>
    /// <typeparam name="string"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class TypedCacheWrapper<TValue> : ITypedCache<TValue>
    {
        public string Name
        {
            get { return InternalCache.Name; }
        }

        public TimeSpan DefaultSlidingExpireTime
        {
            get { return InternalCache.DefaultSlidingExpireTime; }
            set { InternalCache.DefaultSlidingExpireTime = value; }
        }
        public TimeSpan DefaultAbsoluteExpireTime
        {
            get { return InternalCache.DefaultAbsoluteExpireTime; }
            set { InternalCache.DefaultAbsoluteExpireTime = value; }
        }

        public ICache InternalCache { get; private set; }

        /// <summary>
        /// Creates a new <see cref="TypedCacheWrapper{string,TValue}"/> object.
        /// </summary>
        /// <param name="internalCache">The actual internal cache</param>
        public TypedCacheWrapper(ICache internalCache)
        {
            InternalCache = internalCache;
        }

        public void Dispose()
        {
            InternalCache.Dispose();
        }

        public void Clear()
        {
            InternalCache.Clear();
        }

        public Task ClearAsync()
        {
            return InternalCache.ClearAsync();
        }

        public TValue Get(string key, Func<string, TValue> factory)
        {
            return InternalCache.Get(key, factory);
        }

        public TValue[] Get(string[] keys, Func<string, TValue> factory)
        {
            return InternalCache.Get(keys, factory);
        }

        public Task<TValue> GetAsync(string key, Func<string, Task<TValue>> factory)
        {
            return InternalCache.GetAsync(key, factory);
        }

        public Task<TValue[]> GetAsync(string[] keys, Func<string, Task<TValue>> factory)
        {
            return InternalCache.GetAsync(keys, factory);
        }

        public TValue GetOrDefault(string key)
        {
            return InternalCache.GetOrDefault<string, TValue>(key);
        }

        public TValue[] GetOrDefault(string[] keys)
        {
            return InternalCache.GetOrDefault<string, TValue>(keys);
        }

        public Task<TValue> GetOrDefaultAsync(string key)
        {
            return InternalCache.GetOrDefaultAsync<string, TValue>(key);
        }

        public Task<TValue[]> GetOrDefaultAsync(string[] keys)
        {
            return InternalCache.GetOrDefaultAsync<string, TValue>(keys);
        }

        public void Set(string key, TValue value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null)
        {
            InternalCache.Set(key.ToString(), value, slidingExpireTime, absoluteExpireTime);
        }

        public void Set(KeyValuePair<string, TValue>[] pairs, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null)
        {
            var stringPairs = pairs.Select(p => new KeyValuePair<string, object>(p.Key.ToString(), p.Value));
            InternalCache.Set(stringPairs.ToArray(), slidingExpireTime, absoluteExpireTime);
        }

        public Task SetAsync(string key, TValue value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null)
        {
            return InternalCache.SetAsync(key.ToString(), value, slidingExpireTime, absoluteExpireTime);
        }

        public Task SetAsync(KeyValuePair<string, TValue>[] pairs, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null)
        {
            var stringPairs = pairs.Select(p => new KeyValuePair<string, object>(p.Key.ToString(), p.Value));
            return InternalCache.SetAsync(stringPairs.ToArray(), slidingExpireTime, absoluteExpireTime);
        }

        public void Remove(string key)
        {
            InternalCache.Remove(key.ToString());
        }

        public void Remove(string[] keys)
        {
            InternalCache.Remove(keys.Select(key => key.ToString()).ToArray());
        }

        public Task RemoveAsync(string key)
        {
            return InternalCache.RemoveAsync(key.ToString());
        }

        public Task RemoveAsync(string[] keys)
        {
            return InternalCache.RemoveAsync(keys.Select(key => key.ToString()).ToArray());
        }
    }
}
