using System.Security.Claims;

namespace AspCoreMicroservice.Core.Security.Claims
{
    public class ServiceAccountPrincipalProvider : IClaimsPrincipalProvider
    {
        private readonly ITokenProvider tokenProvider;

        public ServiceAccountPrincipalProvider(ITokenProvider tokenProvider)
        {
            this.tokenProvider = tokenProvider;
        } 

        public ClaimsPrincipal CurrentPrincipal
        {
            get
            {
                var accessToken = tokenProvider.GetAccessTokenAsync().GetAwaiter().GetResult();
                if (accessToken == null)
                    return null;

                var identity = new ClaimsIdentity(AuthenticationTypes.Federation);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, accessToken.Subject));
                identity.AddClaim(new Claim(AppClaimTypes.Issuer, accessToken.Issuer));
                identity.AddClaim(new Claim(AppClaimTypes.AccessToken, accessToken.RawData));

                return new ClaimsPrincipal(identity);
            }
        }
    }
}
