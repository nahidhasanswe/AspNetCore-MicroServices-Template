// ASP.NET Boilerplate - Web Application Framework https://aspnetboilerplate.com
// Copyright (c) 2013-2017 Volosoft (https://volosoft.com)
// This code is licensed under MIT license (see LICENSE.txt for details)
using System;
using System.Threading.Tasks;

namespace AspCoreMicroservice.Core.Runtime.Caching
{
    /// <summary>
    /// Extension methods for <see cref="ITypedCache{TKey,TValue}"/>.
    /// </summary>
    public static class TypedCacheExtensions
    {
        public static TValue Get<TValue>(this ITypedCache<TValue> cache, string key, Func<TValue> factory)
        {
            return cache.Get(key, k => factory());
        }

        public static Task<TValue> GetAsync<TValue>(this ITypedCache<TValue> cache, string key, Func<Task<TValue>> factory)
        {
            return cache.GetAsync(key, k => factory());
        }
    }
}
