using System.Security.Claims;

namespace AspCoreMicroservice.Core.Security.Claims
{
    public interface IClaimsPrincipalProvider
    {
        ClaimsPrincipal CurrentPrincipal { get; }
    }
}
