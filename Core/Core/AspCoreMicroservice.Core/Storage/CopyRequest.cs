namespace AspCoreMicroservice.Core.Storage
{
    public class CopyRequest
    {
        public string SourceStorageName { get; set; }
        public string SourceKeyName { get; set; }
        public string DestinationStorageName { get; set; }
        public string DestinationKeyName { get; set; }
    }
}
