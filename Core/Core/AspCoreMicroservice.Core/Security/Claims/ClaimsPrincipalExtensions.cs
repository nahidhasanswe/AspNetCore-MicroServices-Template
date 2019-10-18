using AspCoreMicroservice.Core.Security.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace AspCoreMicroservice.Core.Security.Claims
{
    public static class ClaimsPrincipalExtensions
    {

        public static ClaimsIdentity GetIdentity(this ClaimsPrincipal principal, string provider, string providerKey)
        {
            if (principal == null || string.IsNullOrEmpty(provider) || string.IsNullOrEmpty(providerKey))
                return null;

            return principal.Identities.FirstOrDefault(i => i.GetIdentityProvider() == provider && i.GetId() == providerKey);
        }

        public static ClaimsIdentity GetIdentity(this ClaimsPrincipal principal, string authenticationType = AuthenticationTypes.Default)
        {
            return principal.Identities.FirstOrDefault(i => i.AuthenticationType == authenticationType);
        }

        public static string GetId(this ClaimsPrincipal principal, string authenticationType = AuthenticationTypes.Default)
        {
            var identity = principal.GetIdentity(authenticationType);
            if (identity == null)
                return null;

            return identity.GetId();
        }

        public static string GetIdentityProvider(this ClaimsPrincipal principal, string authenticationType = AuthenticationTypes.Default)
        {
            var identity = principal.Identities.FirstOrDefault(i => i.AuthenticationType == authenticationType);
            if (identity == null)
                return null;

            return identity.GetIdentityProvider();
        }

        public static string GetAccessToken(this ClaimsPrincipal principal)
        {
            foreach(ClaimsIdentity identity in principal.Identities)
            {
                var accessToken = identity.FindFirst(AppClaimTypes.AccessToken)?.Value;
                if (accessToken != null)
                    return accessToken;
            }

            return null;
        }
  
        public static void AddClaim(this ClaimsPrincipal principal, Claim claim)
        {
            var claimsIdentity = (ClaimsIdentity)principal.Identity;
            if (!claimsIdentity.HasClaim(claim.Type, claim.Value))
                claimsIdentity.AddClaim(claim);
        }

        public static void AddClaims(this ClaimsPrincipal principal, IEnumerable<Claim> claims)
        {
            var claimsIdentity = (ClaimsIdentity)principal.Identity;

            foreach (var current in claims)
            {
                if (!claimsIdentity.HasClaim(current.Type, current.Value))
                    claimsIdentity.AddClaim(current);
            }
        }

        public static bool HasPermission(this ClaimsPrincipal principal, params string[] permissions)
        {
            if (permissions == null)
                return false;

            var userPermissions = principal.Claims.Where(c => c.Type == AppClaimTypes.Permission).Select(c => Permission.FromString(c.Value));
            foreach (var permission in permissions)
            {
                if (userPermissions.Contains(Permission.FromString(permission)))
                    return true;
            }

            return false;
        }

      
    }
}
