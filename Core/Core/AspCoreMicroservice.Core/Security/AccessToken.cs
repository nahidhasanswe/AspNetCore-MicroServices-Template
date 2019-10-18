using System;

namespace AspCoreMicroservice.Core.Security
{
    public abstract class AccessToken
    {
        public string Id { get; protected set; }
        public string Subject { get; protected set; }
        public string Issuer { get; protected set; }
        public string RawData { get; protected set; }
        public DateTime ValidTo { get; protected set; }
    }
}
