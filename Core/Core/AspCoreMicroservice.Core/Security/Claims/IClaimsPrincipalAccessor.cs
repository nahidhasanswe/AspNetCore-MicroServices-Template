using System.Security.Claims;

namespace AspCoreMicroservice.Core.Security.Claims
{
    public interface IClaimsPrincipalAccessor
    {
        ClaimsPrincipal GetCurrentPrincipal();
    }
}
