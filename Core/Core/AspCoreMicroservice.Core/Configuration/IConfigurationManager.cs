using System.Threading.Tasks;

namespace AspCoreMicroservice.Core.Configuration
{
    public interface IConfigurationManager
    {
        Task<string> GetSettingAsync(string key, string defaultValue=null);
    }
}
