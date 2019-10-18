// ASP.NET Boilerplate - Web Application Framework https://aspnetboilerplate.com
// Copyright (c) 2013-2017 Volosoft (https://volosoft.com)
// This code is licensed under MIT license (see LICENSE.txt for details)
using AspCoreMicroservice.Core.Runtime.Caching.Configuration;
using Microsoft.Extensions.Logging;

namespace AspCoreMicroservice.Core.Runtime.Caching.Memory
{
    /// <summary>
    /// Implements <see cref="ICacheManager"/> to work with MemoryCache.
    /// </summary>
    public class MemoryCacheManager : CacheManagerBase
    {
        public ILogger Logger { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public MemoryCacheManager(ILogger<MemoryCacheManager> logger, ICachingConfiguration configuration)
            : base(configuration)
        {
            Logger = logger;
        }

        protected override ICache CreateCacheImplementation(string name)
        {
            return new MemoryCache(Logger, name)
            {
                Logger = Logger
            };
        }

        protected override void DisposeCaches()
        {
            foreach (var cache in Caches.Values)
            {
                cache.Dispose();
            }
        }
    }
}
