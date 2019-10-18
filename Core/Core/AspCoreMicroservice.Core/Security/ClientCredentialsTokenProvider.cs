using AspCoreMicroservice.Core.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AspCoreMicroservice.Core.Security
{  
    public class ClientCredentialsTokenProvider : ITokenProvider
    {
        protected IMemoryCache Cache { get; }
        protected HttpClient HttpClient { get; }

        protected OAuthClientSettings Settings { get; }

        public ClientCredentialsTokenProvider(IMemoryCache cache, HttpClient httpClient, IOptions<OAuthClientSettings> settings)
        {
            Cache = cache;
            HttpClient = httpClient;
            Settings = settings.Value;
        }

        public async Task<AccessToken> GetAccessTokenAsync()
        {
            var cacheKey = $"OAuthClient_{Settings.ClientId}_{Settings.Audience}";
            var token = Cache?.Get<AccessToken>(cacheKey);
            if (token == null)
            {
                var credentials = new AccessTokenRequest
                {
                    GrantType = "client_credentials",
                    ClientId = Settings.ClientId,
                    ClientSecret = Settings.ClientSecret,
                    Audience = Settings.Audience
                };

                var result = await HttpClient.PostAsync(Settings.TokenEndpoint, new StringContent(credentials.ToJsonString(), Encoding.UTF8, "application/json"));
                result.EnsureSuccessStatusCode();

                var content = await result.Content.ReadAsStringAsync();
                var oauthToken = content.FromJsonString<Token>();

                // Convert token to JwtSecurityToken
                var handler = new JwtSecurityTokenHandler();
                token = new JwtAccessToken(handler.ReadToken(oauthToken.RawToken) as JwtSecurityToken);

                if (Cache != null)
                    Cache.Set(cacheKey, token, new DateTimeOffset(token.ValidTo));
            }

            return token;
        }

        class Token
        {
            [JsonPropertyName("access_token")]
            public string RawToken { get; set; }

            [JsonPropertyName("token_type")]
            public string TokenType { get; set; }

            [JsonPropertyName("expires_in")]
            public int ExpiresIn { get; set; }
        }
    }
}
