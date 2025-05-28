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
    /// Provides operations for system management via the CodeSandbox API.
    /// </summary>
    public class SystemService : ISystemService
    {
        private readonly IApiClient _client;
        private readonly LoggerService _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemService"/> class.
        /// </summary>
        /// <param name="IApiClient">The HTTP client used to communicate with the CodeSandbox API. Cannot be null.</param>
        /// <param name="logger">The logger service for diagnostic output. Cannot be null.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="IApiClient"/> or <paramref name="logger"/> is <c>null</c>.</exception>
        public SystemService(IApiClient client, LoggerService logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? new LoggerService(LogLevel.Trace);
        }

        /// <summary>
        /// Sends a request to update the system asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="SandboxSystemSuccessResponse"/> indicating the result of the update operation.
        /// </returns>
        /// <exception cref="ApiException">Thrown when the API returns an error response.</exception>
        /// <exception cref="Exception">Thrown on HTTP failure, JSON deserialization failure, or unexpected errors.</exception>
        public async Task<SandboxSystemSuccessResponse> UpdateSystemAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("UpdateSystemAsync called.");
            try
            {
                string url = "/system/update";
                var payload = new { }; // Empty object payload

                SandboxSystemSuccessResponse response = await _client.PostAsync<SandboxSystemSuccessResponse>(url, payload, cancellationToken);

                _logger.LogSuccess("UpdateSystemAsync succeeded.");
                return response;
            }
            catch (ApiException apiEx)
            {
                _logger.LogError($"API error in UpdateSystemAsync: {apiEx.Message}");
                throw;
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError($"HTTP request failed in UpdateSystemAsync: {httpEx.Message}");
                throw new Exception("HTTP request failed in UpdateSystemAsync.", httpEx);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in UpdateSystemAsync: {ex.Message}");
                throw new Exception("Unexpected error in UpdateSystemAsync.", ex);
            }
        }

        /// <summary>
        /// Sends a request to hibernate the system asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="SandboxSystemSuccessResponse"/> indicating the result of the hibernate operation.
        /// </returns>
        /// <exception cref="ApiException">Thrown when the API returns an error response.</exception>
        /// <exception cref="Exception">Thrown on HTTP failure, JSON deserialization failure, or unexpected errors.</exception>
        public async Task<SandboxSystemSuccessResponse> HibernateSystemAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("HibernateSystemAsync called.");
            try
            {
                string url = "/system/hibernate";
                var payload = new { }; // Empty object payload

                SandboxSystemSuccessResponse response = await _client.PostAsync<SandboxSystemSuccessResponse>(url, payload, cancellationToken);

                _logger.LogSuccess("HibernateSystemAsync succeeded.");
                return response;
            }
            catch (ApiException apiEx)
            {
                _logger.LogError($"API error in HibernateSystemAsync: {apiEx.Message}");
                throw;
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError($"HTTP request failed in HibernateSystemAsync: {httpEx.Message}");
                throw new Exception("HTTP request failed in HibernateSystemAsync.", httpEx);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in HibernateSystemAsync: {ex.Message}");
                throw new Exception("Unexpected error in HibernateSystemAsync.", ex);
            }
        }

        /// <summary>
        /// Retrieves system metrics asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="SandboxSystemMetricsStatus"/> containing CPU, memory, and storage metrics.
        /// </returns>
        /// <exception cref="ApiException">Thrown when the API returns an error response.</exception>
        /// <exception cref="Exception">Thrown on HTTP failure, JSON deserialization failure, or unexpected errors.</exception>
        public async Task<SandboxSystemMetricsStatus> GetSystemMetricsAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("GetSystemMetricsAsync called.");
            try
            {
                string url = "/system/metrics";
                var payload = new { }; // Empty object payload

                SandboxSystemSuccessResponse wrapper = await _client.PostAsync<SandboxSystemSuccessResponse>(url, payload, cancellationToken);
                var metrics = JsonConvert.DeserializeObject<SandboxSystemMetricsStatus>(wrapper.Result.ToString());

                _logger.LogSuccess("GetSystemMetricsAsync succeeded.");
                return metrics;
            }
            catch (ApiException apiEx)
            {
                _logger.LogError($"API error in GetSystemMetricsAsync: {apiEx.Message}");
                throw;
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError($"HTTP request failed in GetSystemMetricsAsync: {httpEx.Message}");
                throw new Exception("HTTP request failed in GetSystemMetricsAsync.", httpEx);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in GetSystemMetricsAsync: {ex.Message}");
                throw new Exception("Unexpected error in GetSystemMetricsAsync.", ex);
            }
        }
    }
}