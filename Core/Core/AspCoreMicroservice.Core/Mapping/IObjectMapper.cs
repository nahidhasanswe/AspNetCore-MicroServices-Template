// ASP.NET Boilerplate - Web Application Framework https://aspnetboilerplate.com
// Copyright (c) 2013-2017 Volosoft (https://volosoft.com)
// This code is licensed under MIT license (see LICENSE.txt for details)
namespace AspCoreMicroservice.Core.Mapping
{
    /// <summary>
    /// Defines a simple interface to map objects.
    /// </summary>
    public interface IObjectMapper
    {
        /// <summary>
        /// Converts an object to another. Creates a new object of <see cref="TDestination"/>.
        /// </summary>
        /// <typeparam name="TDestination">Type of the destination object</typeparam>
        /// <param name="source">Source object</param>
        TDestination Map<TDestination>(object source);

        /// <summary>
        /// Execute a mapping from the source object to the existing destination object
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TDestination">Destination type</typeparam>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <returns>Returns the same <see cref="destination"/> object after mapping operation</returns>
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}
