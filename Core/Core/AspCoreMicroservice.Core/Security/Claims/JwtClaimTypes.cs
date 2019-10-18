namespace AspCoreMicroservice.Core.Security.Claims
{
    public static class JwtClaimTypes
    {
        public const string Type = "typ";
        public const string ExpirationTime = "exp";
        public const string NotBefore = "nbf";
        public const string IssuedAt = "iat";
        public const string Issuer = "iss";
        public const string Audience = "aud";
        public const string Principal = "prn";
        public const string Id = "jti";
    }
}
