using System.IO.Pipelines;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Models.New.SandboxFSModels;

namespace CodeSandbox.SDK.Net.Interfaces
{
    /// <summary>
    /// Interface for file system related operations via the sandbox FS API.
    /// </summary>
    public interface ISandboxFsService
    {
        /// <summary>
        /// Asynchronously writes a file.
        /// </summary>
        /// <param name="request">The request parameters for writing a file.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task returning a success response with an optional result object.</returns>
        Task<object> WriteFileAsync(SandboxFSWriteFileRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously searches paths in the file system.
        /// </summary>
        /// <param name="request">Parameters for path search.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task returning a success response with the path search result.</returns>
        Task<SandboxFSPathSearchResult> FsPathSearchAsync(SandboxFSPathSearchResult request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously uploads a file.
        /// </summary>
        /// <param name="request">The upload request parameters.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task returning a success response with upload result details.</returns>
        Task<UploadResult> FsUploadAsync(UploadRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously downloads a file.
        /// </summary>
        /// <param name="request">The download request parameters.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task returning a success response with download result details.</returns>
        Task<DownloadResult> FsDownloadAsync(DownloadRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously reads a file.
        /// </summary>
        /// <param name="request">Parameters for reading a file.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task returning a success response with the file read result.</returns>
        Task<FSReadFileResult> FsReadFileAsync(FSReadFileParams request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously reads a directory.
        /// </summary>
        /// <param name="request">Parameters for reading a directory.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task returning a success response with the directory read result.</returns>
        Task<FSReadDirResult> ReadDirAsync(FSReadDirParams request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously retrieves file or directory metadata (stat).
        /// </summary>
        /// <param name="request">Parameters for retrieving file/directory stats.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task returning a success response with the stat result.</returns>
        Task<FSStatResult> StatAsync(FSStatParams request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously copies files or directories.
        /// </summary>
        /// <param name="request">Parameters specifying source and destination for the copy operation.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task returning a success response with an optional result object.</returns>
        Task<object> CopyAsync(FSCopyParams request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously renames files or directories.
        /// </summary>
        /// <param name="request">Parameters specifying the rename operation.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task returning a success response with an optional result object.</returns>
        Task<object> RenameAsync(FSRenameParams request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously removes files or directories.
        /// </summary>
        /// <param name="request">Parameters specifying the remove operation.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task returning a success response with an optional result object.</returns>
        Task<object> RemoveAsync(FSRemoveParams request, CancellationToken cancellationToken = default);

        Task<FSReadResult> FsReadAsync(CancellationToken cancellationToken = default);
        Task<FSOperationResult> FsOperationAsync(FSOperationRequest request, CancellationToken cancellationToken = default);
        Task<SearchResult[]> FsSearchAsync(FSSearchParams request, CancellationToken cancellationToken = default);
        Task<FSStreamingSearchResult> FsStreamingSearchAsync(FSStreamingSearchParams request, CancellationToken cancellationToken = default);
        Task<FSCancelStreamingSearchResult> FsCancelStreamingSearchAsync(FSCancelStreamingSearchParams request, CancellationToken cancellationToken = default);
        Task<object> MkdirAsync(FSMkdirParams request, CancellationToken cancellationToken = default);
        Task<FSWatchResult> WatchAsync(FSWatchParams request, CancellationToken cancellationToken = default);
        Task<object> UnwatchAsync(FSUnwatchParams request, CancellationToken cancellationToken = default);
    }
}
