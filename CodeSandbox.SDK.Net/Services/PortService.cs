using System;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Interfaces;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models.New.PortModels;

namespace CodeSandbox.SDK.Net.Services
{
    /// <summary>
    /// Provides operations for retrieving port information from the CodeSandbox API.
    /// </summary>
    public class PortService : IPortService
    {
        private readonly IApiClient _client;
        private readonly LoggerService _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PortService"/> class.
        /// </summary>
        /// <param name="client">The API client instance used to communicate with the CodeSandbox API. Cannot be null.</param>
        /// <param name="logger">Optional logger instance for diagnostic output. If not provided, a default logger is used.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="client"/> is null.</exception>
        public PortService(IApiClient client, LoggerService logger = null)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? new LoggerService(LogLevel.Trace);
        }

        /// <summary>
        /// Asynchronously retrieves the list of ports and their associated URLs from the API.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task representing the asynchronous operation, with a <see cref="PortListResult"/> containing the list of ports and URLs.
        /// </returns>
        /// <exception cref="Exception">Thrown if the API call fails or an unexpected error occurs.</exception>
        public async Task<PortListResult> GetPortListAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting GetPortListAsync...");
            try
            {
                PortSuccessResponse response = await _client.PostAsync<PortSuccessResponse>("/port/list", new { }, cancellationToken);
                _logger.LogSuccess("GetPortListAsync completed successfully.");
                return response?.Result;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in GetPortListAsync: {ex.Message} (Status: {ex.StatusCode})");
#if DEBUG
                _logger.LogTrace($"DEBUG - API Response Content: {ex.Message}");
#endif
                throw new Exception($"API error while fetching port list: {ex.Message} (Status: {ex.StatusCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in GetPortListAsync.", ex);
#if DEBUG
                _logger.LogTrace($"DEBUG - Exception Details: {ex.GetType().Name}: {ex.Message}\n{ex.StackTrace}");
#endif
                throw new Exception($"Unexpected error while fetching port list: {ex.Message}", ex);
            }
        }
    }
}