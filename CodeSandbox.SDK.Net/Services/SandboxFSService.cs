using System;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Models;
using CodeSandbox.SDK.Net.Interfaces;
using CodeSandbox.SDK.Net.Internal;

namespace CodeSandbox.SDK.Services
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
        /// <param name="apiClient">The API client instance (required).</param>
        /// <param name="logger">Optional logger instance.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="apiClient"/> is null.</exception>
        public SandboxFsService(ApiClient apiClient, LoggerService logger = null)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            _logger = logger ?? new LoggerService(LogLevel.Trace);
        }

        public async Task<SuccessResponse<object>> WriteFileAsync(WriteFileRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting WriteFileAsync...");
            try
            {
                var result = await _apiClient.PostAsync<SuccessResponse<object>>("/fs/writeFile", request, cancellationToken);
                _logger.LogSuccess("WriteFileAsync completed successfully.");
                return result;
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

        public async Task<SuccessResponse<PathSearchResult>> FsPathSearchAsync(PathSearchParams request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting FsPathSearchAsync...");
            try
            {
                var result = await _apiClient.PostAsync<SuccessResponse<PathSearchResult>>("/fs/pathSearch", request, cancellationToken);
                _logger.LogSuccess("FsPathSearchAsync completed successfully.");
                return result;
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

        public async Task<SuccessResponse<UploadResult>> FsUploadAsync(UploadRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting FsUploadAsync...");
            try
            {
                var result = await _apiClient.PostAsync<SuccessResponse<UploadResult>>("/fs/upload", request, cancellationToken);
                _logger.LogSuccess("FsUploadAsync completed successfully.");
                return result;
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

        public async Task<SuccessResponse<DownloadResult>> FsDownloadAsync(DownloadRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting FsDownloadAsync...");
            try
            {
                var result = await _apiClient.PostAsync<SuccessResponse<DownloadResult>>("/fs/download", request, cancellationToken);
                _logger.LogSuccess("FsDownloadAsync completed successfully.");
                return result;
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

        public async Task<SuccessResponse<FSReadFileResult>> FsReadFileAsync(FSReadFileParams request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting FsReadFileAsync...");
            try
            {
                var result = await _apiClient.PostAsync<SuccessResponse<FSReadFileResult>>("/fs/readFile", request, cancellationToken);
                _logger.LogSuccess("FsReadFileAsync completed successfully.");
                return result;
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

        public async Task<SuccessResponse<FSReadDirResult>> ReadDirAsync(FSReadDirParams request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting ReadDirAsync...");
            try
            {
                var result = await _apiClient.PostAsync<SuccessResponse<FSReadDirResult>>("/fs/readdir", request, cancellationToken);
                _logger.LogSuccess("ReadDirAsync completed successfully.");
                return result;
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

        public async Task<SuccessResponse<FSStatResult>> StatAsync(FSStatParams request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting StatAsync...");
            try
            {
                var result = await _apiClient.PostAsync<SuccessResponse<FSStatResult>>("/fs/stat", request, cancellationToken);
                _logger.LogSuccess("StatAsync completed successfully.");
                return result;
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

        public async Task<SuccessResponse<object>> CopyAsync(FSCopyParams request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting CopyAsync...");
            try
            {
                var result = await _apiClient.PostAsync<SuccessResponse<object>>("/fs/copy", request, cancellationToken);
                _logger.LogSuccess("CopyAsync completed successfully.");
                return result;
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

        public async Task<SuccessResponse<object>> RenameAsync(FSRenameParams request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting RenameAsync...");
            try
            {
                var result = await _apiClient.PostAsync<SuccessResponse<object>>("/fs/rename", request, cancellationToken);
                _logger.LogSuccess("RenameAsync completed successfully.");
                return result;
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

        public async Task<SuccessResponse<object>> RemoveAsync(FSRemoveParams request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting RemoveAsync...");
            try
            {
                var result = await _apiClient.PostAsync<SuccessResponse<object>>("/fs/remove", request, cancellationToken);
                _logger.LogSuccess("RemoveAsync completed successfully.");
                return result;
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
