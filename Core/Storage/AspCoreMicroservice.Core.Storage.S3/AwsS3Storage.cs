using System;
using System.Threading;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;

namespace AspCoreMicroservice.Core.Storage.S3
{
    public class AwsS3StorageManager : IStorageManager
    {
        private readonly IAmazonS3 storageClient;

        public AwsS3StorageManager(IAmazonS3 storageClient)
        {
            this.storageClient = storageClient;
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
                CopyObjectRequest request = new CopyObjectRequest
                {
                    SourceBucket = copyRequest.SourceStorageName,
                    SourceKey = copyRequest.SourceKeyName,
                    DestinationBucket = copyRequest.DestinationStorageName,
                    DestinationKey = copyRequest.DestinationKeyName,
                };

                CopyObjectResponse response = await storageClient.CopyObjectAsync(request);

                return new StorageResponse
                {
                    Key = copyRequest.DestinationKeyName,
                    StorageName = copyRequest.DestinationStorageName
                };
            }
            catch (Exception exception)
            {
                throw new StorageException($"Failed to copy file '{copyRequest.SourceKeyName}' to AWS S3 bucket '{copyRequest.DestinationStorageName}'.", copyRequest.SourceKeyName, copyRequest.DestinationStorageName, exception);
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
                var request = new DeleteObjectRequest
                {
                    Key = deleteRequest.KeyName,
                    BucketName = deleteRequest.StorageName,
                };

                await storageClient.DeleteObjectAsync(request, cancellationToken);

                await Task.CompletedTask;
            }
            catch (Exception except)
            {
                throw new StorageException($"Failed to delete file '{deleteRequest.KeyName}' to AWS S3 bucket '{deleteRequest.StorageName}'.", deleteRequest.KeyName, deleteRequest.StorageName, except);
            }
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

            }
            catch (Exception exception)
            {
                throw new StorageException($"Failed to rename file '{renameRequest.OldKeyName}' to AWS S3 bucket '{renameRequest.StorageName}'.", renameRequest.OldKeyName, renameRequest.StorageName, exception);
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

            var permission = S3CannedACL.PublicRead;

            if (request.Permission == StoragePermission.NoPermission)
                permission = S3CannedACL.NoACL;

            if (request.Permission == StoragePermission.Private)
                permission = S3CannedACL.Private;


            try
            {
                var requestUpload = new TransferUtilityUploadRequest
                {
                    Key = request.Key,
                    InputStream = request.InputStream,
                    ContentType = request.ContentType,
                    BucketName = request.StorageName,
                    CannedACL = permission,
                    StorageClass = S3StorageClass.Standard,
                };

                if (request.Metadata != null)
                {
                    foreach (var current in request.Metadata)
                    {
                        if (!string.IsNullOrEmpty(current.Value))
                            request.Metadata.Add(current.Key, current.Value);
                    }
                }

                using (var transferClient = new TransferUtility(storageClient))
                {
                    await transferClient.UploadAsync(requestUpload, cancellationToken);

                    return new StorageResponse 
                    { 
                        Key = requestUpload.Key,
                        StorageName = requestUpload.BucketName
                    };
                }
            }
            catch (Exception except)
            {
                throw new StorageException($"Failed to upload file '{request.Key}' to AWS S3 bucket '{request.StorageName}'.", request.Key, request.StorageName, except);
            }
        }

        public async Task<bool> IsStorageExistAsync(string storageName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(storageName))
                throw new StorageException("The storage name was missing");

            return await AmazonS3Util.DoesS3BucketExistV2Async(storageClient, storageName);
        }

        public async Task<bool> IsFileExistAsync(string key, string storageName, CancellationToken cancellationToken = default)
        {
            try
            {
                GetObjectRequest getObjectRequest = new GetObjectRequest
                {
                    Key = key,
                    BucketName = storageName
                };

                var response = await storageClient.GetObjectAsync(getObjectRequest);

                if (response.ResponseStream != null)
                    return true;

                return false;

            } catch
            {
                return false;
            }
        }
    }
}
