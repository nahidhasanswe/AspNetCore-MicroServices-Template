// ASP.NET Boilerplate - Web Application Framework https://aspnetboilerplate.com
// Copyright (c) 2013-2017 Volosoft (https://volosoft.com)
// This code is licensed under MIT license (see LICENSE.txt for details)
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace AspCoreMicroservice.Core.Runtime.Caching.Memory
{
    /// <summary>
    /// Implements <see cref="ICache"/> to work with <see cref="MemoryCache"/>.
    /// </summary>
    public class MemoryCache : CacheBase
    {
        private Microsoft.Extensions.Caching.Memory.MemoryCache _memoryCache;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Unique name of the cache</param>
        public MemoryCache(ILogger logger, string name)
            : base(logger, name)
        {
            _memoryCache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new OptionsWrapper<MemoryCacheOptions>(new MemoryCacheOptions()));
        }

        public override object GetOrDefault(string key)
        {
            return _memoryCache.Get(key);
        }

        public override void Set(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null)
        {
            Check.NotNull(value, "value");
            if (absoluteExpireTime != null)
            {
                _memoryCache.Set(key, value, DateTimeOffset.Now.Add(absoluteExpireTime.Value));
            }
            else if (slidingExpireTime != null)
            {
                _memoryCache.Set(key, value, slidingExpireTime.Value);
            }
            else if (DefaultAbsoluteExpireTime > TimeSpan.MinValue)
            {
                _memoryCache.Set(key, value, DateTimeOffset.Now.Add(DefaultAbsoluteExpireTime));
            }
            else
            {
                _memoryCache.Set(key, value, DefaultSlidingExpireTime);
            }
        }

        public override void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public override void Clear()
        {
            _memoryCache.Dispose();
            _memoryCache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new OptionsWrapper<MemoryCacheOptions>(new MemoryCacheOptions()));
        }

        public override void Dispose()
        {
            _memoryCache.Dispose();
            base.Dispose();
        }
    }
}
