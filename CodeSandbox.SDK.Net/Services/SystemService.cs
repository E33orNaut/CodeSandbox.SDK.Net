using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Interfaces;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models.New.SandboxSystemModels;
using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Services
{
    /// <summary>
    /// Provides system-related API operations.
    /// </summary>
    public class SystemService : ISystemService
    {
        private readonly HttpClient _httpClient;
        private readonly LoggerService _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemService"/> class.
        /// </summary>
        /// <param name="httpClient">HTTP client for sending requests.</param>
        /// <param name="logger">Logger service for logging.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="httpClient"/> or <paramref name="logger"/> is <c>null</c>.</exception>
        public SystemService(HttpClient httpClient, LoggerService logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Sends a request to update the system asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A <see cref="SandboxSystemSuccessResponse"/> indicating the result.</returns>
        /// <exception cref="ApiException">Thrown when the API returns an error response.</exception>
        /// <exception cref="Exception">Thrown on HTTP failure, JSON deserialization failure, or unexpected errors.</exception>
        public async Task<SandboxSystemSuccessResponse> UpdateSystemAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("UpdateSystemAsync called.");
            try
            {
                string url = "/system/update";
                StringContent content = new StringContent("{}", Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content, cancellationToken);
                string json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<SandboxSystemSuccessResponse>(json);
                    _logger.LogSuccess("UpdateSystemAsync succeeded.");
                    return result;
                }
                else
                {
                    var error = JsonConvert.DeserializeObject<SandboxSystemErrorResponse>(json);
                    _logger.LogError($"API error in UpdateSystemAsync: Code={error.Error?.Code}, Message={error.Error?.Message}");
                    throw new ApiException(
                        error.Error?.Message ?? "API error",
                        (int)response.StatusCode,
                        json,
                        error
                    );
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError($"HTTP request failed in UpdateSystemAsync: {httpEx.Message}");
                throw new Exception("HTTP request failed in UpdateSystemAsync.", httpEx);
            }
            catch (Exception ex) when (!(ex is ApiException))
            {
                _logger.LogError($"Unexpected error in UpdateSystemAsync: {ex.Message}");
                throw new Exception("Unexpected error in UpdateSystemAsync.", ex);
            }
        }

        /// <summary>
        /// Sends a request to hibernate the system asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A <see cref="SandboxSystemSuccessResponse"/> indicating the result.</returns>
        /// <exception cref="ApiException">Thrown when the API returns an error response.</exception>
        /// <exception cref="Exception">Thrown on HTTP failure, JSON deserialization failure, or unexpected errors.</exception>
        public async Task<SandboxSystemSuccessResponse> HibernateSystemAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("HibernateSystemAsync called.");
            try
            {
                string url = "/system/hibernate";
                StringContent content = new StringContent("{}", Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content, cancellationToken);
                string json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<SandboxSystemSuccessResponse>(json);
                    _logger.LogSuccess("HibernateSystemAsync succeeded.");
                    return result;
                }
                else
                {
                    var error = JsonConvert.DeserializeObject<SandboxSystemErrorResponse>(json);
                    _logger.LogError($"API error in HibernateSystemAsync: Code={error.Error?.Code}, Message={error.Error?.Message}");
                    throw new ApiException(
                        error.Error?.Message ?? "API error",
                        (int)response.StatusCode,
                        json,
                        error
                    );
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError($"HTTP request failed in HibernateSystemAsync: {httpEx.Message}");
                throw new Exception("HTTP request failed in HibernateSystemAsync.", httpEx);
            }
            catch (Exception ex) when (!(ex is ApiException))
            {
                _logger.LogError($"Unexpected error in HibernateSystemAsync: {ex.Message}");
                throw new Exception("Unexpected error in HibernateSystemAsync.", ex);
            }
        }

        /// <summary>
        /// Retrieves system metrics asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A <see cref="SandboxSystemMetricsStatus"/> containing system metrics.</returns>
        /// <exception cref="ApiException">Thrown when the API returns an error response.</exception>
        /// <exception cref="Exception">Thrown on HTTP failure, JSON deserialization failure, or unexpected errors.</exception>
        public async Task<SandboxSystemMetricsStatus> GetSystemMetricsAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("GetSystemMetricsAsync called.");
            try
            {
                string url = "/system/metrics";
                StringContent content = new StringContent("{}", Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content, cancellationToken);
                string json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var wrapper = JsonConvert.DeserializeObject<SandboxSystemSuccessResponse>(json);
                    var metrics = JsonConvert.DeserializeObject<SandboxSystemMetricsStatus>(wrapper.Result.ToString());
                    _logger.LogSuccess("GetSystemMetricsAsync succeeded.");
                    return metrics;
                }
                else
                {
                    var error = JsonConvert.DeserializeObject<SandboxSystemErrorResponse>(json);
                    _logger.LogError($"API error in GetSystemMetricsAsync: Code={error.Error?.Code}, Message={error.Error?.Message}");
                    throw new ApiException(
                        error.Error?.Message ?? "API error",
                        (int)response.StatusCode,
                        json,
                        error
                    );
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError($"HTTP request failed in GetSystemMetricsAsync: {httpEx.Message}");
                throw new Exception("HTTP request failed in GetSystemMetricsAsync.", httpEx);
            }
            catch (Exception ex) when (!(ex is ApiException))
            {
                _logger.LogError($"Unexpected error in GetSystemMetricsAsync: {ex.Message}");
                throw new Exception("Unexpected error in GetSystemMetricsAsync.", ex);
            }
        }
    }
}
