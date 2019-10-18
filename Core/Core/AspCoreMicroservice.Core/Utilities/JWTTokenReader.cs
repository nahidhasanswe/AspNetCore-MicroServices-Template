using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace AspCoreMicroservice.Core.Utilities
{
    public static class JWTTokenReader
    {
        public static string GetTokenValue(string ClaimType, string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var tokenClaims = handler.ReadToken(token) as JwtSecurityToken;
            return tokenClaims.Claims.First(claim => claim.Type == ClaimType).Value;
        }
    }
}
