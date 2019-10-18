using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AspCoreMicroservice.Core.Storage.Blob
{
    public class AzureBlobStorage : IStorageManager
    {
        private readonly CloudBlobClient cloudBlobClient;
        public AzureBlobStorage(CloudBlobClient cloudBlobClient)
        {
            this.cloudBlobClient = cloudBlobClient;
        }

        public async Task<StorageResponse> CopyAsync(CopyRequest copyRequest, CancellationToken cancellationToken = default)
        {
            if (copyRequest == null)
            {
                throw new StorageException("Copy request is missing");
            }

            if (string.IsNullOrEmpty(copyRequest.SourceKeyName))
            {
                throw new StorageException("Source storage key name is missing");
            }

            if (string.IsNullOrEmpty(copyRequest.SourceStorageName))
            {
                throw new StorageException("Source storage name is missing");
            }

            if (string.IsNullOrEmpty(copyRequest.DestinationKeyName))
            {
                throw new StorageException("Destination storage key name is missing");
            }

            if (string.IsNullOrEmpty(copyRequest.DestinationStorageName))
            {
                throw new StorageException("Destination storage name is missing");
            }

            try
            {
                var sourceBlob = await GetBlobReferenceAsync(copyRequest.SourceStorageName, copyRequest.SourceKeyName);

                var destinationContainer = await GetContainerReferenceAsync(copyRequest.DestinationStorageName);
                var destinationBlob = destinationContainer.GetBlobReference(copyRequest.SourceKeyName);

                await destinationBlob.StartCopyAsync(sourceBlob.Uri);

                while (destinationBlob.CopyState.Status == CopyStatus.Pending)
                {
                    await Task.Delay(200);
                    await destinationBlob.FetchAttributesAsync();
                }

                if (destinationBlob.CopyState.Status != CopyStatus.Success)
                {
                    throw new StorageException($"Failed to copy file '{copyRequest.SourceKeyName}' to Azure blob storage '{copyRequest.DestinationStorageName}'.", copyRequest.SourceKeyName, copyRequest.DestinationStorageName);
                }

                return new StorageResponse
                {
                    Key = copyRequest.DestinationKeyName,
                    StorageName = copyRequest.DestinationStorageName
                };
            }
            catch (Exception exception)
            {
                throw new StorageException($"Failed to copy file '{copyRequest.SourceKeyName}' to Azure blob storage '{copyRequest.DestinationStorageName}'.", copyRequest.SourceKeyName, copyRequest.DestinationStorageName, exception);
            }
        }

        public async Task DeleteAsync(DeleteRequest deleteRequest, CancellationToken cancellationToken = default)
        {
            if (deleteRequest == null)
            {
                throw new StorageException("Delete request is missing");
            }

            if (string.IsNullOrEmpty(deleteRequest.KeyName))
            {
                throw new StorageException("Storage key name is missing");
            }

            if (string.IsNullOrEmpty(deleteRequest.StorageName))
            {
                throw new StorageException("Storage name is missing");
            }

            try
            {
                var blob = await GetBlobReferenceAsync(deleteRequest.StorageName, deleteRequest.KeyName);
                await blob.DeleteIfExistsAsync();

                await Task.CompletedTask;
            }
            catch (Exception except)
            {
                throw new StorageException($"Failed to delete file '{deleteRequest.KeyName}' to azure blob storage '{deleteRequest.StorageName}'.", deleteRequest.KeyName, deleteRequest.StorageName, except);
            }
        }

        public async Task<bool> IsStorageExistAsync(string storageName, CancellationToken cancellationToken = default)
        {
            var container = await GetContainerReferenceAsync(storageName);

            return await container.ExistsAsync();
        }

        public async Task<StorageResponse> RenameAsync(RenameRequest renameRequest, CancellationToken cancellationToken = default)
        {
            if (renameRequest == null)
            {
                throw new StorageException("Rename request is missing");
            }

            if (string.IsNullOrEmpty(renameRequest.OldKeyName))
            {
                throw new StorageException("Old storage key name is missing");
            }

            if (string.IsNullOrEmpty(renameRequest.NewKeyName))
            {
                throw new StorageException("New storage name is missing");
            }

            if (string.IsNullOrEmpty(renameRequest.StorageName))
            {
                throw new StorageException("Storage name is missing");
            }

           try
            {
                var response = await CopyAsync(new CopyRequest
                {
                    SourceKeyName = renameRequest.OldKeyName,
                    DestinationKeyName = renameRequest.NewKeyName,
                    DestinationStorageName = renameRequest.StorageName,
                    SourceStorageName = renameRequest.StorageName
                });

                await DeleteAsync(new DeleteRequest
                {
                    KeyName = renameRequest.OldKeyName,
                    StorageName = renameRequest.StorageName
                });

                return response;

            } catch (Exception exception)
            {
                throw new StorageException($"Failed to rename file '{renameRequest.OldKeyName}' to Azure blob storage '{renameRequest.StorageName}'.", renameRequest.OldKeyName, renameRequest.StorageName, exception);
            }
        }

        public async Task<StorageResponse> UploadAsync(UploadRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new StorageException("Upload request is missing");
            }

            if (string.IsNullOrEmpty(request.Key))
            {
                throw new StorageException("Storage key name is missing");
            }

            if (string.IsNullOrEmpty(request.StorageName))
            {
                throw new StorageException("Storage name is missing");
            }

            if (request.InputStream == null)
            {
                throw new StorageException("Invalid input stream");
            }

            try
            {
                var blob = await GetBlobReferenceAsync(request.StorageName, request.Key);

                blob.Properties.ContentType = request.ContentType;

                if (request.Metadata != null)
                {
                    foreach (var current in request.Metadata)
                    {
                        if (!string.IsNullOrEmpty(current.Value))
                            blob.Metadata.Add(current.Key, current.Value);
                    }
                }

                await blob.UploadFromStreamAsync(request.InputStream);

                return new StorageResponse
                {
                    Key = request.Key,
                    StorageName = request.StorageName
                };

            } catch (Exception except)
            {
                throw new StorageException($"Failed to upload file '{request.Key}' to Azure blob storage '{request.StorageName}'.", request.Key, request.StorageName, except);
            }



        }

        private async Task<CloudBlobContainer> GetContainerReferenceAsync(string containerName)
        {
            try
            {
                var container = cloudBlobClient.GetContainerReference(containerName);

                if (container == null)
                    throw new StorageException($"The storage '{containerName}' does not exist", containerName);


                BlobContainerPermissions permissions = new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                };

                await container.SetPermissionsAsync(permissions);

                return container;

            }
            catch (Exception ex)
            {
                throw new StorageException($"The storage '{containerName}' does not exist", containerName, ex);
            }
        }

        private async Task<ICloudBlob> GetBlobReferenceAsync(string containerName, string keyName)
        {
            try
            {
                var container = await GetContainerReferenceAsync(containerName);

                var blob = await container.GetBlobReferenceFromServerAsync(keyName);
                return blob;
            }
            catch (Exception ex)
            {
                throw new StorageException($"The blob '{keyName}' create failed", containerName, keyName, ex);
            }
        }

        public async Task<bool> IsFileExistAsync(string key, string storageName, CancellationToken cancellationToken = default)
        {
            var blob = await GetBlobReferenceAsync(storageName, key);
            return await blob.ExistsAsync();
        }
    }
}
