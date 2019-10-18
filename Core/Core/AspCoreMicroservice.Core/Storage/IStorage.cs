using System.Threading;
using System.Threading.Tasks;

namespace AspCoreMicroservice.Core.Storage
{
    public interface IStorageManager
    {
        Task<StorageResponse> UploadAsync(UploadRequest request, CancellationToken cancellationToken = default(CancellationToken));
        Task<StorageResponse> CopyAsync(CopyRequest copyRequest, CancellationToken cancellationToken = default(CancellationToken));
        Task<StorageResponse> RenameAsync(RenameRequest renameRequest, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteAsync(DeleteRequest deleteRequest, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> IsStorageExistAsync(string storageName, CancellationToken cancellationToken = default);
        Task<bool> IsFileExistAsync(string key, string storageName, CancellationToken cancellationToken = default(CancellationToken));
    }
}
