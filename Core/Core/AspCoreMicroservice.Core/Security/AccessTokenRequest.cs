﻿using System.Text.Json.Serialization;

namespace AspCoreMicroservice.Core.Security
{
    internal class AccessTokenRequest
    {
        [JsonPropertyName("grant_type")]
        public string GrantType { get; set; }

        [JsonPropertyName("client_id")]
        public string ClientId { get; set; }

        [JsonPropertyName("client_secret")]
        public string ClientSecret { get; set; }

        [JsonPropertyName("audience")]
        public string Audience { get; set; }
    }
}
