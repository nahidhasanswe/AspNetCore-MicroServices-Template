using Microsoft.Extensions.DependencyInjection;

namespace AspCoreMicroservice.Core.ServiceCollection
{
    public static class CommonServiceCollection
    {
        public static IServiceCollection AddLiveCommon(this IServiceCollection services)
        {
            services.AddSingleton<ICryptoGraphy, CryptoGraphy>();
            return services;
        }
    }
}
