using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Interfaces;
using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Internal
{



    /// <summary>
    /// Exception thrown when API calls fail.
    /// Contains status code, response content, and optional detailed error info.
    /// </summary>
    public class ApiException : Exception
    {
        /// <summary>
        /// Gets the HTTP status code returned by the API.
        /// </summary>
        public int StatusCode { get; }

        /// <summary>
        /// Gets the raw response content from the API.
        /// </summary>
        public string ResponseContent { get; }

        /// <summary>
        /// Gets the deserialized error details from the API response, if any.
        /// </summary>
        public object ApiErrorDetails { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="statusCode">HTTP status code.</param>
        /// <param name="responseContent">Raw response content.</param>
        /// <param name="apiErrorDetails">Deserialized error details (optional).</param>
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
    /// Supports GET, POST, PUT, and DELETE requests with JSON serialization and detailed error handling.
    /// </summary>
    public class ApiClient : IApiClient, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly LoggerService _logger;
        private bool _disposed;

        private const int DefaultMaxRetries = 3;
        private const int DefaultDelayMs = 1000;

        /// <summary>
        /// /// Validates that the provided path is not null or empty.
        /// </summary>
        /// <param name="path"></param>
        /// <exception cref="ArgumentException"></exception>
        public void ValidatePathT(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Path cannot be null or empty.", nameof(path));
            }
        }

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
        /// Sends a GET request asynchronously and deserializes the JSON response to type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type to deserialize the response content to.</typeparam>
        /// <param name="path">API endpoint path relative to base URL.</param>
        /// <param name="cancellationToken">Cancellation token (optional).</param>
        /// <returns>Deserialized response of type <typeparamref name="T"/>.</returns>
        /// <exception cref="ApiException">Thrown if response is not JSON or unsuccessful.</exception>
        public async Task<T> GetAsync<T>(string path, CancellationToken cancellationToken = default)
        {
            ValidatePath(path);
            _logger.LogInfo($"GET: {path}");

            return await WithNetworkRetryAsync(async () =>
            {
                HttpResponseMessage response = await _httpClient.GetAsync(path, cancellationToken).ConfigureAwait(false);
                string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                _logger.LogTrace($"GET Response: {(int)response.StatusCode} - {Truncate(content)}");

                if (!IsJsonResponse(response))
                {
                    throw new ApiException("GET failed: Response content type is not JSON", (int)response.StatusCode, content, null);
                }

                if (!response.IsSuccessStatusCode)
                {
                    object errorDetails = TryParseErrorDetails(content);
                    throw new ApiException($"GET failed: {(int)response.StatusCode}", (int)response.StatusCode, content, errorDetails);
                }

                return JsonConvert.DeserializeObject<T>(content);
            }, "GET", path);
        }

        /// <summary>
        /// Sends a POST request asynchronously with a JSON payload and deserializes the JSON response to type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type to deserialize the response content to.</typeparam>
        /// <param name="path">API endpoint path relative to base URL.</param>
        /// <param name="payload">Object payload to serialize to JSON and send.</param>
        /// <param name="cancellationToken">Cancellation token (optional).</param>
        /// <returns>Deserialized response of type <typeparamref name="T"/>.</returns>
        /// <exception cref="ApiException">Thrown if response is not JSON or unsuccessful.</exception>
        public async Task<T> PostAsync<T>(string path, object payload, CancellationToken cancellationToken = default)
        {
            ValidatePath(path);
            _logger.LogInfo($"POST: {path}");
            _logger.LogTrace($"Payload: {JsonConvert.SerializeObject(payload)}");

            return await WithNetworkRetryAsync(async () =>
            {
                string json = JsonConvert.SerializeObject(payload);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(path, content, cancellationToken).ConfigureAwait(false);
                string responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                _logger.LogTrace($"POST Response: {(int)response.StatusCode} - {Truncate(responseContent)}");

                if (!IsJsonResponse(response))
                {
                    throw new ApiException("POST failed: Response content type is not JSON", (int)response.StatusCode, responseContent, null);
                }

                if (!response.IsSuccessStatusCode)
                {
                    object errorDetails = TryParseErrorDetails(responseContent);
                    throw new ApiException($"POST failed: {(int)response.StatusCode}", (int)response.StatusCode, responseContent, errorDetails);
                }

                return JsonConvert.DeserializeObject<T>(responseContent);
            }, "POST", path);
        }

        /// <summary>
        /// Sends a PUT request asynchronously with a JSON payload and deserializes the JSON response to type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type to deserialize the response content to.</typeparam>
        /// <param name="path">API endpoint path relative to base URL.</param>
        /// <param name="payload">Object payload to serialize to JSON and send.</param>
        /// <param name="cancellationToken">Cancellation token (optional).</param>
        /// <returns>Deserialized response of type <typeparamref name="T"/>.</returns>
        /// <exception cref="ApiException">Thrown if response is not JSON or unsuccessful.</exception>
        public async Task<T> PutAsync<T>(string path, object payload, CancellationToken cancellationToken = default)
        {
            ValidatePath(path);
            _logger.LogInfo($"PUT: {path}");
            _logger.LogTrace($"Payload: {JsonConvert.SerializeObject(payload)}");

            return await WithNetworkRetryAsync(async () =>
            {
                string json = JsonConvert.SerializeObject(payload);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync(path, content, cancellationToken).ConfigureAwait(false);
                string responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                _logger.LogTrace($"PUT Response: {(int)response.StatusCode} - {Truncate(responseContent)}");

                if (!IsJsonResponse(response))
                {
                    throw new ApiException("PUT failed: Response content type is not JSON", (int)response.StatusCode, responseContent, null);
                }

                if (!response.IsSuccessStatusCode)
                {
                    object errorDetails = TryParseErrorDetails(responseContent);
                    throw new ApiException($"PUT failed: {(int)response.StatusCode}", (int)response.StatusCode, responseContent, errorDetails);
                }

                return JsonConvert.DeserializeObject<T>(responseContent);
            }, "PUT", path);
        }

        /// <summary>
        /// Sends a DELETE request asynchronously.
        /// </summary>
        /// <param name="path">API endpoint path relative to base URL.</param>
        /// <param name="cancellationToken">Cancellation token (optional).</param>
        /// <returns>A task representing the asynchronous delete operation.</returns>
        /// <exception cref="ApiException">Thrown if the response status is unsuccessful.</exception>
        public async Task DeleteAsync(string path, CancellationToken cancellationToken = default)
        {
            ValidatePath(path);
            _logger.LogInfo($"DELETE: {path}");

            await WithNetworkRetryAsync(async () =>
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
            }, "DELETE", path);
        }

        /// <summary>
        /// Disposes the ApiClient and its resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Implements the dispose pattern to release managed and unmanaged resources.
        /// </summary>
        /// <param name="disposing">Indicates whether the method is called from Dispose.</param>
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

        /// <summary>
        /// Logs exception details with method and path context.
        /// </summary>
        /// <param name="method">HTTP method used.</param>
        /// <param name="path">API endpoint path.</param>
        /// <param name="ex">Exception to log.</param>
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

        /// <summary>
        /// Truncates a string to a maximum length with ellipsis if necessary.
        /// </summary>
        /// <param name="input">Input string to truncate.</param>
        /// <param name="max">Maximum length allowed.</param>
        /// <returns>Truncated string with ellipsis if longer than max.</returns>
        private string Truncate(string input, int max = 200)
        {
            return string.IsNullOrWhiteSpace(input) ? string.Empty : input.Length <= max ? input : input.Substring(0, max) + "...";
        }

        /// <summary>
        /// Validates the API path is not null or empty.
        /// </summary>
        /// <param name="path">API path to validate.</param>
        /// <exception cref="ArgumentException">Thrown if path is null or whitespace.</exception>
        private void ValidatePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Path cannot be null or empty.", nameof(path));
            }
        }

        /// <summary>
        /// Checks if the HTTP response content type is JSON.
        /// </summary>
        /// <param name="response">HTTP response message.</param>
        /// <returns>True if Content-Type is application/json, false otherwise.</returns>
        private bool IsJsonResponse(HttpResponseMessage response)
        {
            return (response.Content?.Headers?.ContentType) != null && response.Content.Headers.ContentType.MediaType.Equals("application/json", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Attempts to parse error details from JSON content.
        /// </summary>
        /// <param name="content">Response content string.</param>
        /// <returns>Deserialized error object or null if parsing fails.</returns>
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

        /// <summary>
        /// Executes an action with retry logic for network-related exceptions.
        /// </summary>
        /// <typeparam name="T">Type of the result returned by the action.</typeparam>
        /// <param name="action">Action to execute.</param>
        /// <param name="method">HTTP method used.</param>
        /// <param name="path">API endpoint path.</param>
        /// <param name="maxRetries">Maximum number of retries.</param>
        /// <param name="delayMs">Delay between retries in milliseconds.</param>
        /// <returns>Result of the action.</returns>
        private async Task<T> WithNetworkRetryAsync<T>(Func<Task<T>> action, string method, string path, int maxRetries = DefaultMaxRetries, int delayMs = DefaultDelayMs)
        {
            int attempt = 0;
            for (; ; )
            {
                try
                {
                    return await action().ConfigureAwait(false);
                }
                catch (HttpRequestException ex)
                {
                    attempt++;
                    _logger.LogWarning($"{method} {path}: Network error (attempt {attempt}): {ex.Message}");
                    if (attempt > maxRetries)
                    {
                        throw;
                    }
                }
                catch (TaskCanceledException ex) // Handles timeouts
                {
                    attempt++;
                    _logger.LogWarning($"{method} {path}: Timeout (attempt {attempt}): {ex.Message}");
                    if (attempt > maxRetries)
                    {
                        throw;
                    }
                }
                await Task.Delay(delayMs).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Executes an action with retry logic for network-related exceptions.
        /// </summary>
        /// <param name="action">Action to execute.</param>
        /// <param name="method">HTTP method used.</param>
        /// <param name="path">API endpoint path.</param>
        /// <param name="maxRetries">Maximum number of retries.</param>
        /// <param name="delayMs">Delay between retries in milliseconds.</param>
        private async Task WithNetworkRetryAsync(Func<Task> action, string method, string path, int maxRetries = DefaultMaxRetries, int delayMs = DefaultDelayMs)
        {
            _ = await WithNetworkRetryAsync<object>(async () => { await action(); return null; }, method, path, maxRetries, delayMs);
        }
    }
}
