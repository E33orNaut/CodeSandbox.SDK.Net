using System;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Interfaces;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models;

namespace CodeSandbox.SDK.Net.Services
{
    /// <summary>
    /// Service for retrieving port information.
    /// </summary>
    public class PortService : IPortService
    {
        private readonly ApiClient _client;
        private readonly LoggerService _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="PortService"/>.
        /// </summary>
        /// <param name="client">API client instance (required).</param>
        /// <param name="logger">Optional logger instance.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="client"/> is null.</exception>
        public PortService(ApiClient client, LoggerService logger = null)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? new LoggerService(LogLevel.Trace);
        }

        /// <inheritdoc />
        public async Task<PortListResponse> GetPortListAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting GetPortListAsync...");
            try
            {
                PortListResponse result = await _client.PostAsync<PortListResponse>("/port/list", new { }, cancellationToken);
                _logger.LogSuccess("GetPortListAsync completed successfully.");
                return result;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in GetPortListAsync: {ex.Message} (Status: {ex.ErrorCode})");
#if DEBUG
                _logger.LogTrace($"DEBUG - API Response Content: {ex.Message}");
#endif
                throw new Exception($"API error while fetching port list: {ex.Message} (Status: {ex.ErrorCode})", ex);
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
