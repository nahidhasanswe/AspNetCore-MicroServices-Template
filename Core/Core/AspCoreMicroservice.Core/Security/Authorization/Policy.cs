using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace AspCoreMicroservice.Core.Security.Authorization
{
    public class Policy
    {
        public Policy() { }
        public Policy(IList<Permission> permissions)
        {
            Permissions = permissions?.ToArray();
        }

        /// <summary>
        /// List of application permissions associated with policy
        /// </summary>
        [JsonPropertyName("permissions")]
        public Permission[] Permissions { get; set; }
    }
}
