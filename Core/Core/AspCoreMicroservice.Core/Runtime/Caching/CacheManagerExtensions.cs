// ASP.NET Boilerplate - Web Application Framework https://aspnetboilerplate.com
// Copyright (c) 2013-2017 Volosoft (https://volosoft.com)
// This code is licensed under MIT license (see LICENSE.txt for details)
namespace AspCoreMicroservice.Core.Runtime.Caching
{
    /// <summary>
    /// Extension methods for <see cref="ICacheManager"/>.
    /// </summary>
    public static class CacheManagerExtensions
    {
        public static ITypedCache<TValue> GetCache<TValue>(this ICacheManager cacheManager, string name)
        {
            return cacheManager.GetCache(name).AsTyped<TValue>();
        }
    }
}
