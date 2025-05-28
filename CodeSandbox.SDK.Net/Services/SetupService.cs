using System;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Models.New.SandboxSetupModels;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Interfaces;

namespace CodeSandbox.SDK.Net.Services
{
    /// <summary>
    /// Provides operations for managing the sandbox setup process via the CodeSandbox API.
    /// </summary>
    public class SetupService : ISetupService
    {
        private readonly IApiClient _client;
        private readonly LoggerService _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetupService"/> class.
        /// </summary>
        /// <param name="client">The API client instance used to communicate with the CodeSandbox API. Cannot be null.</param>
        /// <param name="logger">Optional logger instance for diagnostic output. If not provided, a default logger is used.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="client"/> is null.</exception>
        public SetupService(IApiClient client, LoggerService logger = null)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? new LoggerService(LogLevel.Trace);
        }

        /// <summary>
        /// Retrieves the current setup progress status.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="SandboxSetupSuccessResponse"/> containing the status and a <see cref="SandboxSetupProgress"/> result with setup progress details.
        /// </returns>
        /// <exception cref="Exception">Thrown if the API call fails or an unexpected error occurs.</exception>
        public async Task<SandboxSetupSuccessResponse> GetSetupProgressAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting GetSetupProgressAsync...");
            try
            {
                var response = await _client.PostAsync<SandboxSetupSuccessResponse>("/setup/get", new { }, cancellationToken);
                _logger.LogSuccess("GetSetupProgressAsync completed successfully.");
                return response;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in GetSetupProgressAsync: {ex.Message} (Status: {ex.StatusCode})");
                throw new Exception($"API error while getting setup progress: {ex.Message} (Status: {ex.StatusCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in GetSetupProgressAsync.", ex);
                throw new Exception($"Unexpected error while getting setup progress: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Skips a specific step in the setup process.
        /// </summary>
        /// <param name="stepIndexToSkip">The index of the step to skip.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="SandboxSetupSuccessResponse"/> containing the status and updated <see cref="SandboxSetupProgress"/>.
        /// </returns>
        /// <exception cref="Exception">Thrown if the API call fails or an unexpected error occurs.</exception>
        public async Task<SandboxSetupSuccessResponse> SkipStepAsync(int stepIndexToSkip, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo($"Starting SkipStepAsync for step {stepIndexToSkip}...");
            try
            {
                var response = await _client.PostAsync<SandboxSetupSuccessResponse>("/setup/skip", new { stepIndexToSkip }, cancellationToken);
                _logger.LogSuccess("SkipStepAsync completed successfully.");
                return response;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in SkipStepAsync: {ex.Message} (Status: {ex.StatusCode})");
                throw new Exception($"API error while skipping step: {ex.Message} (Status: {ex.StatusCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in SkipStepAsync.", ex);
                throw new Exception($"Unexpected error while skipping step: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Skips all remaining steps in the setup process.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="SandboxSetupSuccessResponse"/> containing the status and updated <see cref="SandboxSetupProgress"/>.
        /// </returns>
        /// <exception cref="Exception">Thrown if the API call fails or an unexpected error occurs.</exception>
        public async Task<SandboxSetupSuccessResponse> SkipAllStepsAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting SkipAllStepsAsync...");
            try
            {
                var response = await _client.PostAsync<SandboxSetupSuccessResponse>("/setup/skipAll", null, cancellationToken);
                _logger.LogSuccess("SkipAllStepsAsync completed successfully.");
                return response;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in SkipAllStepsAsync: {ex.Message} (Status: {ex.StatusCode})");
                throw new Exception($"API error while skipping all steps: {ex.Message} (Status: {ex.StatusCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in SkipAllStepsAsync.", ex);
                throw new Exception($"Unexpected error while skipping all steps: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Disables the setup process.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="SandboxSetupSuccessResponse"/> containing the status and updated <see cref="SandboxSetupProgress"/>.
        /// </returns>
        /// <exception cref="Exception">Thrown if the API call fails or an unexpected error occurs.</exception>
        public async Task<SandboxSetupSuccessResponse> DisableSetupAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting DisableSetupAsync...");
            try
            {
                var response = await _client.PostAsync<SandboxSetupSuccessResponse>("/setup/disable", null, cancellationToken);
                _logger.LogSuccess("DisableSetupAsync completed successfully.");
                return response;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in DisableSetupAsync: {ex.Message} (Status: {ex.StatusCode})");
                throw new Exception($"API error while disabling setup: {ex.Message} (Status: {ex.StatusCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in DisableSetupAsync.", ex);
                throw new Exception($"Unexpected error while disabling setup: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Enables the setup process.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="SandboxSetupSuccessResponse"/> containing the status and updated <see cref="SandboxSetupProgress"/>.
        /// </returns>
        /// <exception cref="Exception">Thrown if the API call fails or an unexpected error occurs.</exception>
        public async Task<SandboxSetupSuccessResponse> EnableSetupAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting EnableSetupAsync...");
            try
            {
                var response = await _client.PostAsync<SandboxSetupSuccessResponse>("/setup/enable", null, cancellationToken);
                _logger.LogSuccess("EnableSetupAsync completed successfully.");
                return response;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in EnableSetupAsync: {ex.Message} (Status: {ex.StatusCode})");
                throw new Exception($"API error while enabling setup: {ex.Message} (Status: {ex.StatusCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in EnableSetupAsync.", ex);
                throw new Exception($"Unexpected error while enabling setup: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Initializes the setup process.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="SandboxSetupSuccessResponse"/> containing the status and initial <see cref="SandboxSetupProgress"/>.
        /// </returns>
        /// <exception cref="Exception">Thrown if the API call fails or an unexpected error occurs.</exception>
        public async Task<SandboxSetupSuccessResponse> InitializeSetupAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting InitializeSetupAsync...");
            try
            {
                var response = await _client.PostAsync<SandboxSetupSuccessResponse>("/setup/init", null, cancellationToken);
                _logger.LogSuccess("InitializeSetupAsync completed successfully.");
                return response;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in InitializeSetupAsync: {ex.Message} (Status: {ex.StatusCode})");
                throw new Exception($"API error while initializing setup: {ex.Message} (Status: {ex.StatusCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in InitializeSetupAsync.", ex);
                throw new Exception($"Unexpected error while initializing setup: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Sets the current step in the setup process.
        /// </summary>
        /// <param name="stepIndex">The index of the step to set as current.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="SandboxSetupSuccessResponse"/> containing the status and updated <see cref="SandboxSetupProgress"/>.
        /// </returns>
        /// <exception cref="Exception">Thrown if the API call fails or an unexpected error occurs.</exception>
        public async Task<SandboxSetupSuccessResponse> SetStepAsync(int stepIndex, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo($"Starting SetStepAsync for step {stepIndex}...");
            try
            {
                var response = await _client.PostAsync<SandboxSetupSuccessResponse>("/setup/setStep", new { stepIndex }, cancellationToken);
                _logger.LogSuccess("SetStepAsync completed successfully.");
                return response;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in SetStepAsync: {ex.Message} (Status: {ex.StatusCode})");
                throw new Exception($"API error while setting step: {ex.Message} (Status: {ex.StatusCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in SetStepAsync.", ex);
                throw new Exception($"Unexpected error while setting step: {ex.Message}", ex);
            }
        }
    }
}