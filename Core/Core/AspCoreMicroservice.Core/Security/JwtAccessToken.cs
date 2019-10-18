using System.IdentityModel.Tokens.Jwt;

namespace AspCoreMicroservice.Core.Security
{
    public class JwtAccessToken : AccessToken
    {
        public JwtAccessToken(JwtSecurityToken token)
        {
            Id = token.Id;
            Subject = token.Subject;
            Issuer = token.Issuer;
            RawData = token.RawData;
            ValidTo = token.ValidTo;
        }
    }
}
