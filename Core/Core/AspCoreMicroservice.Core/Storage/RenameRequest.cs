namespace AspCoreMicroservice.Core.Storage
{
    public class RenameRequest
    {
        public string StorageName { get; set; }
        public string OldKeyName { get; set; }
        public string NewKeyName { get; set; }
    }
}
