using System.Security.Claims;

namespace AspCoreMicroservice.Core.Security.Claims
{
    public class DefaultClaimsPrincipalProvider : IClaimsPrincipalProvider
    {
        public DefaultClaimsPrincipalProvider(ClaimsPrincipal principal)
        {
            CurrentPrincipal = principal;
        }

        public ClaimsPrincipal CurrentPrincipal { get; }
    }
}
