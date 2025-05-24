using System;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Interfaces;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models.New.SandboxContainerModels;
using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Services
{
    /// <summary>
    /// Provides operations for managing container setup and lifecycle.
    /// </summary>
    public class ContainerService : IContainerService
    {
        private readonly ApiClient _client;
        private readonly LoggerService _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerService"/> class.
        /// </summary>
        /// <param name="client">The API client instance used to communicate with the CodeSandbox API. Cannot be null.</param>
        /// <param name="logger">Optional logger instance for diagnostic output. If not provided, a default logger is used.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="client"/> is null.</exception>
        public ContainerService(ApiClient client, LoggerService logger = null)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? new LoggerService(LogLevel.Trace);
        }

        /// <summary>
        /// Sets up a new container using the specified setup request.
        /// </summary>
        /// <param name="request">The <see cref="ContainerSetupRequest"/> containing template, arguments, and features for the container setup.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task representing the asynchronous operation, with a <see cref="ContainerSetupSuccessResponse"/> result containing the status and setup task details.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="request"/> is null.</exception>
        /// <exception cref="ApiException">Thrown if the API returns an error response.</exception>
        /// <exception cref="Exception">Thrown if an unexpected error occurs during setup.</exception>
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
                _logger.LogError($"API error during container setup. Status: {apiEx.StatusCode}, Message: {apiEx.Message}");
#if DEBUG
                _logger.LogTrace($"DEBUG - API Response Content: {apiEx.Message}");
#endif
                throw new Exception(
                    $"API error during container setup: {apiEx.Message} (Status code: {apiEx.StatusCode})",
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