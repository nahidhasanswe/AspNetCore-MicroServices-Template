using AspCoreMicroservice.Core.Json;
using Microsoft.Extensions.Logging;
using System;

namespace AspCoreMicroservice.Core.Runtime.Caching.Distributed
{
    /// <summary>
    ///     Default implementation uses JSON as the underlying persistence mechanism.
    /// </summary>
    public class DefaultCacheSerializer : ICacheSerializer
    {
        private readonly ILogger logger;
        public DefaultCacheSerializer(ILogger<DefaultCacheSerializer> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        ///     Creates an instance of the object from its serialized string representation.
        /// </summary>
        /// <param name="objbyte">String representation of the object from the Redis server.</param>
        /// <returns>Returns a newly constructed object.</returns>
        /// <seealso cref="IRedisCacheSerializer.Serialize" />
        public virtual object Deserialize(string objbyte)
        {
            try
            {
                if (string.IsNullOrEmpty(objbyte))
                    return null;

                CacheData cacheData = CacheData.Deserialize(objbyte);
                return cacheData.Payload.FromJsonString(Type.GetType(cacheData.Type, true, true));
            }
            catch(Exception except)
            {
                logger.LogWarning(except, "Failed to deserialize object.");
                return null;
            }
        }

        /// <summary>
        ///     Produce a string representation of the supplied object.
        /// </summary>
        /// <param name="value">Instance to serialize.</param>
        /// <param name="type">Type of the object.</param>
        /// <returns>Returns a string representing the object instance that can be placed into the Redis cache.</returns>
        /// <seealso cref="IRedisCacheSerializer.Deserialize" />
        public virtual string Serialize(object value, Type type)
        {
            try
            {
                var cacheData = CacheData.Serialize(value);
                return cacheData.ToJsonString();
            }
            catch (Exception except)
            {
                logger.LogWarning(except, $"Failed to serialize object (Type: {type}).");
                throw;
            }
        }
    }
}
