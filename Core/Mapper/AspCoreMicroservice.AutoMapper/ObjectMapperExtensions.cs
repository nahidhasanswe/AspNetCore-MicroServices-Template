using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AspCoreMicroservice.Core.AutoMapper
{
    public static class ObjectMapperExtensions
    {
        public static void AddObjectMapper(this IServiceCollection services, Assembly[] assemblies)
        {
            services.AddAutoMapper(assemblies);
            services.AddTransient<Mapping.IObjectMapper, AutoMapperObjectMapper>();
        }
    }
}
