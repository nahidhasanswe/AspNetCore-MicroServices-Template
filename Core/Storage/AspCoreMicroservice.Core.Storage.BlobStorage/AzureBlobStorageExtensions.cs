using Microsoft.Extensions.DependencyInjection;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AspCoreMicroservice.Core.Storage.Blob
{
    public static class AzureBlobStorageExtensions
    {
        public static IServiceCollection AddAzureBlobStorage(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<CloudBlobClient>(provider =>
            {
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
                return cloudStorageAccount.CreateCloudBlobClient();
            });

            return services;
        }

        public static IServiceCollection AddAzureBlobStorage(this IServiceCollection services, bool UseHttps, string AccountName, string AccountKey, string endPointSuffix)
        {
            return services.AddAzureBlobStorage(GenerateBlobConnectionString(UseHttps, AccountName, AccountKey, endPointSuffix));
        }

        private static string GenerateBlobConnectionString(bool UseHttps, string AccountName, string AccountKey, string endPointSuffix)
        {
            return string.Format("DefaultEndpointsProtocol=http{0};AccountName={1};AccountKey={2};EndpointSuffix:{3}", UseHttps ? "https" : "http", AccountName, AccountKey, endPointSuffix);
        }
    }
}
