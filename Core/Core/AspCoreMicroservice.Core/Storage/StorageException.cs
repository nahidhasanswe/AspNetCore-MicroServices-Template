using System;

namespace AspCoreMicroservice.Core.Storage
{
    public class StorageException : Exception
    {
        public string StorageName { get; set; }
        public string Key { get; set; }
        public StorageException(string message)
            : base(message)
        {

        }

        public StorageException(string message , Exception exception)
            : base(message, exception)
        {

        }

        public StorageException(string message, string storageName)
            : base(message)
        {
            this.StorageName = storageName;
        }

        public StorageException(string message, string storageName, Exception exception)
            : base(message, exception)
        {
            this.StorageName = storageName;
        }

        public StorageException(string message, string storageName, string key)
            : base(message)
        {
            this.StorageName = storageName;
            this.Key = key;
        }

        public StorageException(string message, string storageName, string key, Exception exception)
            : base(message, exception)
        {
            this.StorageName = storageName;
            this.Key = key;
        }
    }
}
