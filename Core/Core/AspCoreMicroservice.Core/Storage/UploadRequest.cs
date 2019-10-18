using System.Collections.Generic;
using System.IO;

namespace AspCoreMicroservice.Core.Storage
{
    public class UploadRequest
    {
        public string Key { get; set; }
        public string StorageName { get; set; }
        public Stream InputStream { get; set; }
        public string ContentType { get; set; }
        public StoragePermission Permission { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
    }
}
