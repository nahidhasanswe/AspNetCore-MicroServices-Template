using AspCoreMicroservice.Core.Runtime.Caching.Configuration;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace AspCoreMicroservice.Core.Runtime.Caching.Distributed
{
    /// <summary>
    /// Used to create <see cref="AbpRedisCache"/> instances.
    /// </summary>
    public class DistributedCacheManager : CacheManagerBase
    {
        private readonly ILogger logger;
        private readonly IDistributedCache cache;
        private readonly ICacheSerializer serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbpRedisCacheManager"/> class.
        /// </summary>
        public DistributedCacheManager(ILogger<DistributedCacheManager> logger, ICachingConfiguration configuration, IDistributedCache cache, ICacheSerializer serializer)
            : base(configuration)
        {
            this.logger = logger;
            this.cache = cache;
            this.serializer = serializer;
        }

        protected override ICache CreateCacheImplementation(string name)
        {
            return new DistributedCache(logger, name, cache, serializer);
        }
    }
}
