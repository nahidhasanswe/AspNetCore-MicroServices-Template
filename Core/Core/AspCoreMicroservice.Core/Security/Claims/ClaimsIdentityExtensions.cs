using AspCoreMicroservice.Core.Security.Authorization;
using System.Linq;
using System.Security.Claims;

namespace AspCoreMicroservice.Core.Security.Claims
{
    public static class ClaimsIdentityExtensions
    {
        public static string GetId(this ClaimsIdentity identity)
        {
            return identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static string GetName(this ClaimsIdentity identity)
        {
            return identity.FindFirst(ClaimTypes.Name)?.Value;
        }

        public static string GetIdentityProvider(this ClaimsIdentity identity)
        {
            return identity.FindFirst(JwtClaimTypes.Issuer)?.Value;
        }

        public static string GetAccessToken(this ClaimsIdentity identity)
        {
            return identity.FindFirst(AppClaimTypes.AccessToken)?.Value;
        }

        public static bool HasPermission(this ClaimsIdentity identity, params string[] permissions)
        {
            if (!identity.IsAuthenticated)
                return false;

            if (permissions == null)
                return false;

            var userPermissions = identity.Claims.Where(c => c.Type == AppClaimTypes.Permission).Select(c => Permission.FromString(c.Value));
            foreach (var permission in permissions)
            {
                if (userPermissions.Contains(Permission.FromString(permission)))
                    return true;
            }

            return false;
        }

        public static bool IsSame(this ClaimsIdentity identity, string provider, string providerKey)
        {
            if (identity == null || string.IsNullOrEmpty(provider) || string.IsNullOrEmpty(providerKey))
                return false;

            return identity.GetIdentityProvider() == provider && identity.GetId() == providerKey;
        }
    }
}
