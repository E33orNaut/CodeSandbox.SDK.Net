using System;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Interfaces;
using CodeSandbox.SDK.Net.Internal; 
using CodeSandbox.SDK.New.Models.New.SandboxContainerModels;
using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Services
{
    /// <summary>
    /// Service to manage container-related operations.
    /// </summary>
    public class ContainerService : IContainerService
    {
        private readonly ApiClient _client;
        private readonly LoggerService _logger;

        /// <summary>
        /// Creates a new instance of <see cref="ContainerService"/>.
        /// </summary>
        /// <param name="client">The API client instance (required).</param>
        /// <param name="logger">Optional logger instance.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="client"/> is null.</exception>
        public ContainerService(ApiClient client, LoggerService logger = null)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? new LoggerService(LogLevel.Trace);
        }

        /// <inheritdoc/>
        public async Task<ContainerSetupSuccessResponse> SetupContainerAsync(ContainerSetupRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInfo("Starting container setup...");
            _logger.LogTrace($"Request Payload: {JsonConvert.SerializeObject(request)}");

            try
            {
                var response = await _client.PostAsync<ContainerSetupSuccessResponse>("/container/setup", request, cancellationToken);
                _logger.LogSuccess("Container setup completed successfully.");
                return response;
            }
            catch (ApiException apiEx)
            {
                _logger.LogError($"API error during container setup. Status: {apiEx.ErrorCode}, Message: {apiEx.Message}");
#if DEBUG
                _logger.LogTrace($"DEBUG - API Response Content: {apiEx.Message}");
#endif
                throw new Exception(
                    $"API error during container setup: {apiEx.Message} (Status code: {apiEx.ErrorCode})",
                    apiEx);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error during container setup.", ex);
#if DEBUG
                _logger.LogTrace($"DEBUG - Exception Details: {ex.GetType().Name}: {ex.Message}\n{ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    _logger.LogTrace($"Inner Exception: {ex.InnerException.GetType().Name}: {ex.InnerException.Message}\n{ex.InnerException.StackTrace}");
                }
#endif
                throw new Exception($"Unexpected error during container setup: {ex.Message}", ex);
            }
        }
    }
}
