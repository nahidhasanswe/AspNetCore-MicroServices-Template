using System.Threading.Tasks;

namespace AspCoreMicroservice.Core.Security
{
    public interface ITokenProvider
    {
        Task<AccessToken> GetAccessTokenAsync();
    }
}
