using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Models;

namespace CodeSandbox.SDK.Net.Interfaces
{
    /// <summary>
    /// Interface for file system related operations via the sandbox FS API.
    /// </summary>
    public interface ISandboxFsService
    {
        Task<SuccessResponse<object>> WriteFileAsync(WriteFileRequest request, CancellationToken cancellationToken = default);

        Task<SuccessResponse<PathSearchResult>> FsPathSearchAsync(PathSearchParams request, CancellationToken cancellationToken = default);

        Task<SuccessResponse<UploadResult>> FsUploadAsync(UploadRequest request, CancellationToken cancellationToken = default);

        Task<SuccessResponse<DownloadResult>> FsDownloadAsync(DownloadRequest request, CancellationToken cancellationToken = default);

        Task<SuccessResponse<FSReadFileResult>> FsReadFileAsync(FSReadFileParams request, CancellationToken cancellationToken = default);

        Task<SuccessResponse<FSReadDirResult>> ReadDirAsync(FSReadDirParams request, CancellationToken cancellationToken = default);

        Task<SuccessResponse<FSStatResult>> StatAsync(FSStatParams request, CancellationToken cancellationToken = default);

        Task<SuccessResponse<object>> CopyAsync(FSCopyParams request, CancellationToken cancellationToken = default);

        Task<SuccessResponse<object>> RenameAsync(FSRenameParams request, CancellationToken cancellationToken = default);

        Task<SuccessResponse<object>> RemoveAsync(FSRemoveParams request, CancellationToken cancellationToken = default);
    }
}
