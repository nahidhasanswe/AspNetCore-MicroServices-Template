// ASP.NET Boilerplate - Web Application Framework https://aspnetboilerplate.com
// Copyright (c) 2013-2017 Volosoft (https://volosoft.com)
// This code is licensed under MIT license (see LICENSE.txt for details)
using System;

namespace AspCoreMicroservice.Core.Runtime.Caching.Configuration
{
    /// <summary>
    /// A registered cache configurator.
    /// </summary>
    public interface ICacheConfigurator
    {
        /// <summary>
        /// Name of the cache.
        /// It will be null if this configurator configures all caches.
        /// </summary>
        string CacheName { get; }

        /// <summary>
        /// Configuration action. Called just after the cache is created.
        /// </summary>
        Action<ICache> InitAction { get; }
    }
}
