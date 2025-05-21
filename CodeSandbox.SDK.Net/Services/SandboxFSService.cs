using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Models;
using CodeSandbox.SDK.Net.Interfaces;
using CodeSandbox.SDK.Net.Internal;
using Newtonsoft.Json;

namespace CodeSandbox.SDK.Services
{
    /// <summary>
    /// Provides file system related operations via the sandbox FS API.
    /// </summary>
    public class SandboxFsService : ISandboxFsService
    {
        private readonly HttpClient _httpClient;
        private readonly LoggerService _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="SandboxFsService"/>.
        /// </summary>
        /// <param name="httpClient">The HTTP client instance (required).</param>
        /// <param name="logger">Optional logger instance.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="httpClient"/> is null.</exception>
        public SandboxFsService(HttpClient httpClient, LoggerService logger = null)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? new LoggerService(LogLevel.Trace);
        }

        public async Task<SuccessResponse<object>> WriteFileAsync(WriteFileRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting WriteFileAsync...");
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync("/fs/writeFile", ToJsonContent(request), cancellationToken);
                SuccessResponse<object> result = await FromJsonAsync<SuccessResponse<object>>(response, cancellationToken);
                _logger.LogSuccess("WriteFileAsync completed successfully.");
                return result;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"HTTP error in WriteFileAsync: {ex.Message}");
#if DEBUG
                _logger.LogTrace(ex.ToString());
#endif
                throw new Exception($"HTTP error in WriteFileAsync: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                _logger.LogError($"Deserialization error in WriteFileAsync: {ex.Message}");
#if DEBUG
                _logger.LogTrace(ex.ToString());
#endif
                throw new Exception($"Deserialization error in WriteFileAsync: {ex.Message}", ex);
            }
        }

        public async Task<SuccessResponse<PathSearchResult>> FsPathSearchAsync(PathSearchParams request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting FsPathSearchAsync...");
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync("/fs/pathSearch", ToJsonContent(request), cancellationToken);
                SuccessResponse<PathSearchResult> result = await FromJsonAsync<SuccessResponse<PathSearchResult>>(response, cancellationToken);
                _logger.LogSuccess("FsPathSearchAsync completed successfully.");
                return result;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"HTTP error in FsPathSearchAsync: {ex.Message}");
#if DEBUG
                _logger.LogTrace(ex.ToString());
#endif
                throw new Exception($"HTTP error in FsPathSearchAsync: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                _logger.LogError($"Deserialization error in FsPathSearchAsync: {ex.Message}");
#if DEBUG
                _logger.LogTrace(ex.ToString());
#endif
                throw new Exception($"Deserialization error in FsPathSearchAsync: {ex.Message}", ex);
            }
        }

        public async Task<SuccessResponse<UploadResult>> FsUploadAsync(UploadRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting FsUploadAsync...");
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync("/fs/upload", ToJsonContent(request), cancellationToken);
                SuccessResponse<UploadResult> result = await FromJsonAsync<SuccessResponse<UploadResult>>(response, cancellationToken);
                _logger.LogSuccess("FsUploadAsync completed successfully.");
                return result;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"HTTP error in FsUploadAsync: {ex.Message}");
#if DEBUG
                _logger.LogTrace(ex.ToString());
#endif
                throw new Exception($"HTTP error in FsUploadAsync: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                _logger.LogError($"Deserialization error in FsUploadAsync: {ex.Message}");
#if DEBUG
                _logger.LogTrace(ex.ToString());
#endif
                throw new Exception($"Deserialization error in FsUploadAsync: {ex.Message}", ex);
            }
        }

        public async Task<SuccessResponse<DownloadResult>> FsDownloadAsync(DownloadRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting FsDownloadAsync...");
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync("/fs/download", ToJsonContent(request), cancellationToken);
                SuccessResponse<DownloadResult> result = await FromJsonAsync<SuccessResponse<DownloadResult>>(response, cancellationToken);
                _logger.LogSuccess("FsDownloadAsync completed successfully.");
                return result;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"HTTP error in FsDownloadAsync: {ex.Message}");
#if DEBUG
                _logger.LogTrace(ex.ToString());
#endif
                throw new Exception($"HTTP error in FsDownloadAsync: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                _logger.LogError($"Deserialization error in FsDownloadAsync: {ex.Message}");
#if DEBUG
                _logger.LogTrace(ex.ToString());
#endif
                throw new Exception($"Deserialization error in FsDownloadAsync: {ex.Message}", ex);
            }
        }

        public async Task<SuccessResponse<FSReadFileResult>> FsReadFileAsync(FSReadFileParams request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting FsReadFileAsync...");
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync("/fs/readFile", ToJsonContent(request), cancellationToken);
                SuccessResponse<FSReadFileResult> result = await FromJsonAsync<SuccessResponse<FSReadFileResult>>(response, cancellationToken);
                _logger.LogSuccess("FsReadFileAsync completed successfully.");
                return result;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"HTTP error in FsReadFileAsync: {ex.Message}");
#if DEBUG
                _logger.LogTrace(ex.ToString());
#endif
                throw new Exception($"HTTP error in FsReadFileAsync: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                _logger.LogError($"Deserialization error in FsReadFileAsync: {ex.Message}");
#if DEBUG
                _logger.LogTrace(ex.ToString());
#endif
                throw new Exception($"Deserialization error in FsReadFileAsync: {ex.Message}", ex);
            }
        }

        public async Task<SuccessResponse<FSReadDirResult>> ReadDirAsync(FSReadDirParams request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting ReadDirAsync...");
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync("/fs/readdir", ToJsonContent(request), cancellationToken);
                SuccessResponse<FSReadDirResult> result = await FromJsonAsync<SuccessResponse<FSReadDirResult>>(response, cancellationToken);
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
                HttpResponseMessage response = await _httpClient.PostAsync("/fs/stat", ToJsonContent(request), cancellationToken);
                SuccessResponse<FSStatResult> result = await FromJsonAsync<SuccessResponse<FSStatResult>>(response, cancellationToken);
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
                HttpResponseMessage response = await _httpClient.PostAsync("/fs/copy", ToJsonContent(request), cancellationToken);
                SuccessResponse<object> result = await FromJsonAsync<SuccessResponse<object>>(response, cancellationToken);
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
                HttpResponseMessage response = await _httpClient.PostAsync("/fs/rename", ToJsonContent(request), cancellationToken);
                SuccessResponse<object> result = await FromJsonAsync<SuccessResponse<object>>(response, cancellationToken);
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
                HttpResponseMessage response = await _httpClient.PostAsync("/fs/remove", ToJsonContent(request), cancellationToken);
                SuccessResponse<object> result = await FromJsonAsync<SuccessResponse<object>>(response, cancellationToken);
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
         
        private StringContent ToJsonContent<T>(T obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
         
        private async Task<T> FromJsonAsync<T>(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            try
            {
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"HTTP error in FromJsonAsync: {ex.Message}");
#if DEBUG
                _logger.LogTrace(ex.ToString());
#endif
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError($"JSON deserialization error in FromJsonAsync: {ex.Message}");
#if DEBUG
                _logger.LogTrace(ex.ToString());
#endif
                throw;
            }
        }
    }
}
