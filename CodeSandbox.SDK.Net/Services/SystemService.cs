using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Interfaces;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models;
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
        /// <returns>A <see cref="SuccessResponse"/> indicating the result.</returns>
        /// <exception cref="ApiException">Thrown when the API returns an error response.</exception>
        /// <exception cref="Exception">Thrown on HTTP failure, JSON deserialization failure, or unexpected errors.</exception>
        public async Task<SuccessResponse> UpdateSystemAsync(CancellationToken cancellationToken = default)
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
                    try
                    {
                        SuccessResponse result = JsonConvert.DeserializeObject<SuccessResponse>(json);
                        _logger.LogSuccess("UpdateSystemAsync succeeded.");
                        return result;
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError($"Failed to deserialize success response JSON in UpdateSystemAsync: {jsonEx.Message}");
                        throw new Exception("Failed to deserialize success response JSON in UpdateSystemAsync.", jsonEx);
                    }
                }
                else
                {
                    try
                    {
                        ErrorResponse error = JsonConvert.DeserializeObject<ErrorResponse>(json);
                        _logger.LogError($"API error in UpdateSystemAsync: Code={error.Error?.Code}, Message={error.Error?.Message}");
                        throw new ApiException(error.Error?.Code.ToString(), error.Error?.Message);
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError($"API error response deserialization failed in UpdateSystemAsync. Status code: {response.StatusCode}. Exception: {jsonEx.Message}");
                        throw new Exception($"API error response deserialization failed in UpdateSystemAsync. Status code: {response.StatusCode}", jsonEx);
                    }
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
        /// <returns>A <see cref="SuccessResponse"/> indicating the result.</returns>
        /// <exception cref="ApiException">Thrown when the API returns an error response.</exception>
        /// <exception cref="Exception">Thrown on HTTP failure, JSON deserialization failure, or unexpected errors.</exception>
        public async Task<SuccessResponse> HibernateSystemAsync(CancellationToken cancellationToken = default)
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
                    try
                    {
                        SuccessResponse result = JsonConvert.DeserializeObject<SuccessResponse>(json);
                        _logger.LogSuccess("HibernateSystemAsync succeeded.");
                        return result;
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError($"Failed to deserialize success response JSON in HibernateSystemAsync: {jsonEx.Message}");
                        throw new Exception("Failed to deserialize success response JSON in HibernateSystemAsync.", jsonEx);
                    }
                }
                else
                {
                    try
                    {
                        ErrorResponse error = JsonConvert.DeserializeObject<ErrorResponse>(json);
                        _logger.LogError($"API error in HibernateSystemAsync: Code={error.Error?.Code}, Message={error.Error?.Message}");
                        throw new ApiException(error.Error?.Code.ToString(), error.Error?.Message);
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError($"API error response deserialization failed in HibernateSystemAsync. Status code: {response.StatusCode}. Exception: {jsonEx.Message}");
                        throw new Exception($"API error response deserialization failed in HibernateSystemAsync. Status code: {response.StatusCode}", jsonEx);
                    }
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
        /// <returns>A <see cref="SuccessResponse"/> containing system metrics.</returns>
        /// <exception cref="ApiException">Thrown when the API returns an error response.</exception>
        /// <exception cref="Exception">Thrown on HTTP failure, JSON deserialization failure, or unexpected errors.</exception>
        public async Task<SuccessResponse> GetSystemMetricsAsync(CancellationToken cancellationToken = default)
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
                    try
                    {
                        SuccessResponseWithMetrics wrapper = JsonConvert.DeserializeObject<SuccessResponseWithMetrics>(json);
                        _logger.LogSuccess("GetSystemMetricsAsync succeeded.");
                        return new SuccessResponse
                        {
                            Status = wrapper.Status,
                            Result = wrapper.Result
                        };
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError($"Failed to deserialize success response JSON in GetSystemMetricsAsync: {jsonEx.Message}");
                        throw new Exception("Failed to deserialize success response JSON in GetSystemMetricsAsync.", jsonEx);
                    }
                }
                else
                {
                    try
                    {
                        ErrorResponse error = JsonConvert.DeserializeObject<ErrorResponse>(json);
                        _logger.LogError($"API error in GetSystemMetricsAsync: Code={error.Error?.Code}, Message={error.Error?.Message}");
                        throw new ApiException(error.Error?.Code.ToString(), error.Error?.Message);
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError($"API error response deserialization failed in GetSystemMetricsAsync. Status code: {response.StatusCode}. Exception: {jsonEx.Message}");
                        throw new Exception($"API error response deserialization failed in GetSystemMetricsAsync. Status code: {response.StatusCode}", jsonEx);
                    }
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

    /// <summary>
    /// Wrapper for the success response containing system metrics.
    /// </summary>
    public class SuccessResponseWithMetrics
    {
        /// <summary>
        /// Gets or sets the status code returned by the API.
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the system metrics result.
        /// </summary>
        [JsonProperty("result")]
        public SystemMetricsStatus Result { get; set; }
    }
}
