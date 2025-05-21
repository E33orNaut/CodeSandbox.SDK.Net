using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Internal
{
    /// <summary>
    /// Exception thrown when API calls fail.
    /// </summary>
    public class ApiException : Exception
    {
        public int StatusCode { get; }
        public string ResponseContent { get; }
        public object ApiErrorDetails { get; }

        public ApiException(string message, int statusCode, string responseContent, object apiErrorDetails = null)
            : base(message)
        {
            StatusCode = statusCode;
            ResponseContent = responseContent;
            ApiErrorDetails = apiErrorDetails;
        }
    }

    /// <summary>
    /// Client to call CodeSandbox API endpoints.
    /// </summary>
    public class ApiClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly LoggerService _logger;
        private bool _disposed;

        /// <summary>
        /// Create a new ApiClient.
        /// </summary>
        /// <param name="baseUrl">Base URL of the API.</param>
        /// <param name="authToken">Bearer token for authentication (optional).</param>
        /// <param name="logger">Logger instance (optional).</param>
        /// <param name="httpClient">Optional HttpClient to reuse externally.</param>
        public ApiClient(
            string baseUrl,
            string authToken = null,
            LoggerService logger = null,
            HttpClient httpClient = null)
        {
            _logger = logger ?? new LoggerService(LogLevel.Trace);

            if (httpClient != null)
            {
                _httpClient = httpClient;
                if (_httpClient.BaseAddress == null)
                {
                    _httpClient.BaseAddress = new Uri(baseUrl);
                }
            }
            else
            {
                _httpClient = new HttpClient
                {
                    BaseAddress = new Uri(baseUrl)
                };
            }

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(authToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", authToken);
            }

            _logger.LogSuccess($"ApiClient initialized with base URL: {baseUrl}");
        }

        /// <summary>
        /// Sends a GET request.
        /// </summary>
        public async Task<T> GetAsync<T>(string path, CancellationToken cancellationToken = default)
        {
            ValidatePath(path);
            _logger.LogInfo($"GET: {path}");

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(path, cancellationToken).ConfigureAwait(false);
                string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                _logger.LogTrace($"GET Response: {(int)response.StatusCode} - {Truncate(content)}");

                if (!IsJsonResponse(response))
                {
                    throw new ApiException("GET failed: Response content type is not JSON", (int)response.StatusCode, content);
                }

                if (!response.IsSuccessStatusCode)
                {
                    object errorDetails = TryParseErrorDetails(content);
                    throw new ApiException($"GET failed: {(int)response.StatusCode}", (int)response.StatusCode, content, errorDetails);
                }

                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                LogException("GET", path, ex);
                throw;
            }
        }

        /// <summary>
        /// Sends a POST request.
        /// </summary>
        public async Task<T> PostAsync<T>(string path, object payload, CancellationToken cancellationToken = default)
        {
            ValidatePath(path);
            _logger.LogInfo($"POST: {path}");
            _logger.LogTrace($"Payload: {JsonConvert.SerializeObject(payload)}");

            try
            {
                string json = JsonConvert.SerializeObject(payload);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(path, content, cancellationToken).ConfigureAwait(false);
                string responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                _logger.LogTrace($"POST Response: {(int)response.StatusCode} - {Truncate(responseContent)}");

                if (!IsJsonResponse(response))
                {
                    throw new ApiException("POST failed: Response content type is not JSON", (int)response.StatusCode, responseContent);
                }

                if (!response.IsSuccessStatusCode)
                {
                    object errorDetails = TryParseErrorDetails(responseContent);
                    throw new ApiException($"POST failed: {(int)response.StatusCode}", (int)response.StatusCode, responseContent, errorDetails);
                }

                return JsonConvert.DeserializeObject<T>(responseContent);
            }
            catch (Exception ex)
            {
                LogException("POST", path, ex);
                throw;
            }
        }

        /// <summary>
        /// Sends a PUT request.
        /// </summary>
        public async Task<T> PutAsync<T>(string path, object payload, CancellationToken cancellationToken = default)
        {
            ValidatePath(path);
            _logger.LogInfo($"PUT: {path}");
            _logger.LogTrace($"Payload: {JsonConvert.SerializeObject(payload)}");

            try
            {
                string json = JsonConvert.SerializeObject(payload);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync(path, content, cancellationToken).ConfigureAwait(false);
                string responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                _logger.LogTrace($"PUT Response: {(int)response.StatusCode} - {Truncate(responseContent)}");

                if (!IsJsonResponse(response))
                {
                    throw new ApiException("PUT failed: Response content type is not JSON", (int)response.StatusCode, responseContent);
                }

                if (!response.IsSuccessStatusCode)
                {
                    object errorDetails = TryParseErrorDetails(responseContent);
                    throw new ApiException($"PUT failed: {(int)response.StatusCode}", (int)response.StatusCode, responseContent, errorDetails);
                }

                return JsonConvert.DeserializeObject<T>(responseContent);
            }
            catch (Exception ex)
            {
                LogException("PUT", path, ex);
                throw;
            }
        }

        /// <summary>
        /// Sends a DELETE request.
        /// </summary>
        public async Task DeleteAsync(string path, CancellationToken cancellationToken = default)
        {
            ValidatePath(path);
            _logger.LogInfo($"DELETE: {path}");

            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync(path, cancellationToken).ConfigureAwait(false);
                string responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                _logger.LogTrace($"DELETE Response: {(int)response.StatusCode} - {Truncate(responseContent)}");

                if (!response.IsSuccessStatusCode)
                {
                    object errorDetails = TryParseErrorDetails(responseContent);
                    throw new ApiException($"DELETE failed: {(int)response.StatusCode}", (int)response.StatusCode, responseContent, errorDetails);
                }

                _logger.LogSuccess($"DELETE successful: {path}");
            }
            catch (Exception ex)
            {
                LogException("DELETE", path, ex);
                throw;
            }
        }

        /// <summary>
        /// Dispose the ApiClient and its resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose pattern implementation.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _logger.LogInfo("Disposing ApiClient.");
                _httpClient?.Dispose();
            }

            _disposed = true;
        }

        private void LogException(string method, string path, Exception ex)
        {
            _logger.LogError($"{method} exception on path: {path}", ex);

#if DEBUG
            _logger.LogTrace($"DEBUG Exception Details:\nType: {ex.GetType().Name}\nMessage: {ex.Message}\nStackTrace: {ex.StackTrace}");
            if (ex.InnerException != null)
            {
                _logger.LogTrace($"Inner Exception:\nType: {ex.InnerException.GetType().Name}\nMessage: {ex.InnerException.Message}\nStackTrace: {ex.InnerException.StackTrace}");
            }
#endif
        }

        private string Truncate(string input, int max = 200)
        {
            return string.IsNullOrWhiteSpace(input) ? string.Empty : input.Length <= max ? input : input.Substring(0, max) + "...";
        }

        private void ValidatePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Path cannot be null or empty.", nameof(path));
            }
        }

        private bool IsJsonResponse(HttpResponseMessage response)
        {
            return (response.Content?.Headers?.ContentType) != null && response.Content.Headers.ContentType.MediaType.Equals("application/json", StringComparison.OrdinalIgnoreCase);
        }

        private object TryParseErrorDetails(string content)
        {
            try
            {
                return JsonConvert.DeserializeObject<object>(content);
            }
            catch
            {
                return null;
            }
        }
    }
}
