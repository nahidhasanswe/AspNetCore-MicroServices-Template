namespace AspCoreMicroservice.Core.Security
{
    public class OAuthClientSettings
    {
        public string Authority { get; set; }
        public string Audience { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string TokenEndpoint { get; set; } = "/oauth/token";
    }
}
