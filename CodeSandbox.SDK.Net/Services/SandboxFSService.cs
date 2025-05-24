using System;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Interfaces;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models.New.SandboxFSModels;

namespace CodeSandbox.SDK.Net.Services
{
    /// <summary>
    /// Provides file system related operations via the sandbox FS API.
    /// </summary>
    public class SandboxFsService : ISandboxFsService
    {
        private readonly ApiClient _apiClient;
        private readonly LoggerService _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="SandboxFsService"/>.
        /// </summary>
        /// <param name="apiClient">The API client instance used to communicate with the CodeSandbox API. Cannot be null.</param>
        /// <param name="logger">Optional logger instance for diagnostic output. If not provided, a default logger is used.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="apiClient"/> is null.</exception>
        public SandboxFsService(ApiClient apiClient, LoggerService logger = null)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            _logger = logger ?? new LoggerService(LogLevel.Trace);
        }

        /// <summary>
        /// Asynchronously writes a file to the sandbox file system.
        /// </summary>
        /// <param name="request">The request parameters for writing a file.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task returning the result object from the write operation, or null if not applicable.
        /// </returns>
        /// <exception cref="Exception">Thrown if the operation fails.</exception>
        public async Task<object> WriteFileAsync(SandboxFSWriteFileRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting WriteFileAsync...");
            try
            {
                var response = await _apiClient.PostAsync<SandboxFSSuccessResponse<object>>("/fs/writeFile", request, cancellationToken);
                _logger.LogSuccess("WriteFileAsync completed successfully.");
                return response?.Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in WriteFileAsync: {ex.Message}", ex);
#if DEBUG
                _logger.LogTrace(ex.ToString());
#endif
                throw new Exception($"Error in WriteFileAsync: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Asynchronously searches for file system paths matching the given criteria.
        /// </summary>
        /// <param name="request">Parameters for path search.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task returning a <see cref="SandboxFSPathSearchResult"/> with the search results.
        /// </returns>
        /// <exception cref="Exception">Thrown if the operation fails.</exception>
        public async Task<SandboxFSPathSearchResult> FsPathSearchAsync(SandboxFSPathSearchResult request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting FsPathSearchAsync...");
            try
            {
                var response = await _apiClient.PostAsync<SandboxFSSuccessResponse<SandboxFSPathSearchResult>>("/fs/pathSearch", request, cancellationToken);
                _logger.LogSuccess("FsPathSearchAsync completed successfully.");
                return response?.Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in FsPathSearchAsync: {ex.Message}", ex);
#if DEBUG
                _logger.LogTrace(ex.ToString());
#endif
                throw new Exception($"Error in FsPathSearchAsync: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Asynchronously uploads a file to the sandbox file system.
        /// </summary>
        /// <param name="request">The upload request parameters.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task returning an <see cref="UploadResult"/> with upload result details.
        /// </returns>
        /// <exception cref="Exception">Thrown if the operation fails.</exception>
        public async Task<UploadResult> FsUploadAsync(UploadRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting FsUploadAsync...");
            try
            {
                var response = await _apiClient.PostAsync<SandboxFSSuccessResponse<UploadResult>>("/fs/upload", request, cancellationToken);
                _logger.LogSuccess("FsUploadAsync completed successfully.");
                return response?.Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in FsUploadAsync: {ex.Message}", ex);
#if DEBUG
                _logger.LogTrace(ex.ToString());
#endif
                throw new Exception($"Error in FsUploadAsync: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Asynchronously downloads a file from the sandbox file system.
        /// </summary>
        /// <param name="request">The download request parameters.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task returning a <see cref="DownloadResult"/> with download result details.
        /// </returns>
        /// <exception cref="Exception">Thrown if the operation fails.</exception>
        public async Task<DownloadResult> FsDownloadAsync(DownloadRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting FsDownloadAsync...");
            try
            {
                var response = await _apiClient.PostAsync<SandboxFSSuccessResponse<DownloadResult>>("/fs/download", request, cancellationToken);
                _logger.LogSuccess("FsDownloadAsync completed successfully.");
                return response?.Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in FsDownloadAsync: {ex.Message}", ex);
#if DEBUG
                _logger.LogTrace(ex.ToString());
#endif
                throw new Exception($"Error in FsDownloadAsync: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Asynchronously reads a file from the sandbox file system.
        /// </summary>
        /// <param name="request">Parameters for reading a file.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task returning an <see cref="FSReadFileResult"/> with the file content.
        /// </returns>
        /// <exception cref="Exception">Thrown if the operation fails.</exception>
        public async Task<FSReadFileResult> FsReadFileAsync(FSReadFileParams request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting FsReadFileAsync...");
            try
            {
                var response = await _apiClient.PostAsync<SandboxFSSuccessResponse<FSReadFileResult>>("/fs/readFile", request, cancellationToken);
                _logger.LogSuccess("FsReadFileAsync completed successfully.");
                return response?.Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in FsReadFileAsync: {ex.Message}", ex);
#if DEBUG
                _logger.LogTrace(ex.ToString());
#endif
                throw new Exception($"Error in FsReadFileAsync: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Asynchronously reads a directory from the sandbox file system.
        /// </summary>
        /// <param name="request">Parameters for reading a directory.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task returning an <see cref="FSReadDirResult"/> with directory entries.
        /// </returns>
        /// <exception cref="Exception">Thrown if the operation fails.</exception>
        public async Task<FSReadDirResult> ReadDirAsync(FSReadDirParams request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting ReadDirAsync...");
            try
            {
                var response = await _apiClient.PostAsync<SandboxFSSuccessResponse<FSReadDirResult>>("/fs/readdir", request, cancellationToken);
                _logger.LogSuccess("ReadDirAsync completed successfully.");
                return response?.Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in ReadDirAsync: {ex.Message}", ex);
#if DEBUG
                _logger.LogTrace(ex.ToString());
#endif
                throw new Exception($"Error in ReadDirAsync: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Asynchronously retrieves file or directory metadata (stat) from the sandbox file system.
        /// </summary>
        /// <param name="request">Parameters for retrieving file/directory stats.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task returning an <see cref="FSStatResult"/> with the stat result.
        /// </returns>
        /// <exception cref="Exception">Thrown if the operation fails.</exception>
        public async Task<FSStatResult> StatAsync(FSStatParams request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting StatAsync...");
            try
            {
                var response = await _apiClient.PostAsync<SandboxFSSuccessResponse<FSStatResult>>("/fs/stat", request, cancellationToken);
                _logger.LogSuccess("StatAsync completed successfully.");
                return response?.Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in StatAsync: {ex.Message}", ex);
#if DEBUG
                _logger.LogTrace(ex.ToString());
#endif
                throw new Exception($"Error in StatAsync: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Asynchronously copies a file or directory in the sandbox file system.
        /// </summary>
        /// <param name="request">Parameters specifying source and destination for the copy operation.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task returning the result object from the copy operation, or null if not applicable.
        /// </returns>
        /// <exception cref="Exception">Thrown if the operation fails.</exception>
        public async Task<object> CopyAsync(FSCopyParams request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting CopyAsync...");
            try
            {
                var response = await _apiClient.PostAsync<SandboxFSSuccessResponse<object>>("/fs/copy", request, cancellationToken);
                _logger.LogSuccess("CopyAsync completed successfully.");
                return response?.Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CopyAsync: {ex.Message}", ex);
#if DEBUG
                _logger.LogTrace(ex.ToString());
#endif
                throw new Exception($"Error in CopyAsync: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Asynchronously renames a file or directory in the sandbox file system.
        /// </summary>
        /// <param name="request">Parameters specifying the rename operation.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task returning the result object from the rename operation, or null if not applicable.
        /// </returns>
        /// <exception cref="Exception">Thrown if the operation fails.</exception>
        public async Task<object> RenameAsync(FSRenameParams request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting RenameAsync...");
            try
            {
                var response = await _apiClient.PostAsync<SandboxFSSuccessResponse<object>>("/fs/rename", request, cancellationToken);
                _logger.LogSuccess("RenameAsync completed successfully.");
                return response?.Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in RenameAsync: {ex.Message}", ex);
#if DEBUG
                _logger.LogTrace(ex.ToString());
#endif
                throw new Exception($"Error in RenameAsync: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Asynchronously removes a file or directory from the sandbox file system.
        /// </summary>
        /// <param name="request">Parameters specifying the remove operation.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task returning the result object from the remove operation, or null if not applicable.
        /// </returns>
        /// <exception cref="Exception">Thrown if the operation fails.</exception>
        public async Task<object> RemoveAsync(FSRemoveParams request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting RemoveAsync...");
            try
            {
                var response = await _apiClient.PostAsync<SandboxFSSuccessResponse<object>>("/fs/remove", request, cancellationToken);
                _logger.LogSuccess("RemoveAsync completed successfully.");
                return response?.Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in RemoveAsync: {ex.Message}", ex);
#if DEBUG
                _logger.LogTrace(ex.ToString());
#endif
                throw new Exception($"Error in RemoveAsync: {ex.Message}", ex);
            }
        }
    }
}