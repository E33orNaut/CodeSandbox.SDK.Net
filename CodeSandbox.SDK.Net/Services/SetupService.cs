using System;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Interfaces;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models;

namespace CodeSandbox.SDK.Net.Services
{
    /// <summary>
    /// Service for managing setup-related API endpoints.
    /// </summary>
    public class SetupService : ISetupService
    {
        private readonly ApiClient _client;
        private readonly LoggerService _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetupService"/> class.
        /// </summary>
        /// <param name="client">API client to send requests.</param>
        /// <param name="logger">Logger service for logging. If <c>null</c>, a default logger with trace level is used.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="client"/> is <c>null</c>.</exception>
        public SetupService(ApiClient client, LoggerService logger = null)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? new LoggerService(LogLevel.Trace);
        }

        /// <summary>
        /// Gets the current setup progress.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>A <see cref="SetupProgress"/> object representing the current setup progress.</returns>
        /// <exception cref="Exception">Throws if the API call fails.</exception>
        public async Task<SetupProgress> GetSetupProgressAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("GetSetupProgressAsync called.");
            try
            {
                ApiResponse<SetupProgress> response = await _client.PostAsync<ApiResponse<SetupProgress>>("/setup/get", null, cancellationToken);
                _logger.LogSuccess("GetSetupProgressAsync succeeded.");
                return response.Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetSetupProgressAsync: {ex.Message}");
                throw new Exception($"Error in GetSetupProgressAsync: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Skips a specific step in the setup process.
        /// </summary>
        /// <param name="stepIndexToSkip">The zero-based index of the step to skip.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>A <see cref="SetupProgress"/> object representing the updated setup progress.</returns>
        /// <exception cref="Exception">Throws if the API call fails.</exception>
        public async Task<SetupProgress> SkipStepAsync(int stepIndexToSkip, CancellationToken cancellationToken = default)
        {
            _logger.LogTrace($"SkipStepAsync called with stepIndexToSkip={stepIndexToSkip}.");
            try
            {
                var payload = new { stepIndexToSkip };
                ApiResponse<SetupProgress> response = await _client.PostAsync<ApiResponse<SetupProgress>>("/setup/skip", payload, cancellationToken);
                _logger.LogSuccess($"SkipStepAsync succeeded for stepIndexToSkip={stepIndexToSkip}.");
                return response.Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in SkipStepAsync: {ex.Message}");
                throw new Exception($"Error in SkipStepAsync: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Skips all setup steps.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>A <see cref="SetupProgress"/> object representing the updated setup progress after skipping all steps.</returns>
        /// <exception cref="Exception">Throws if the API call fails.</exception>
        public async Task<SetupProgress> SkipAllStepsAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("SkipAllStepsAsync called.");
            try
            {
                ApiResponse<SetupProgress> response = await _client.PostAsync<ApiResponse<SetupProgress>>("/setup/skipAll", null, cancellationToken);
                _logger.LogSuccess("SkipAllStepsAsync succeeded.");
                return response.Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in SkipAllStepsAsync: {ex.Message}");
                throw new Exception($"Error in SkipAllStepsAsync: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Disables the setup process.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>A <see cref="SetupProgress"/> object representing the updated setup progress after disabling setup.</returns>
        /// <exception cref="Exception">Throws if the API call fails.</exception>
        public async Task<SetupProgress> DisableSetupAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("DisableSetupAsync called.");
            try
            {
                ApiResponse<SetupProgress> response = await _client.PostAsync<ApiResponse<SetupProgress>>("/setup/disable", null, cancellationToken);
                _logger.LogSuccess("DisableSetupAsync succeeded.");
                return response.Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DisableSetupAsync: {ex.Message}");
                throw new Exception($"Error in DisableSetupAsync: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Enables the setup process.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>A <see cref="SetupProgress"/> object representing the updated setup progress after enabling setup.</returns>
        /// <exception cref="Exception">Throws if the API call fails.</exception>
        public async Task<SetupProgress> EnableSetupAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("EnableSetupAsync called.");
            try
            {
                ApiResponse<SetupProgress> response = await _client.PostAsync<ApiResponse<SetupProgress>>("/setup/enable", null, cancellationToken);
                _logger.LogSuccess("EnableSetupAsync succeeded.");
                return response.Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in EnableSetupAsync: {ex.Message}");
                throw new Exception($"Error in EnableSetupAsync: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Initializes the setup process.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>A <see cref="SetupProgress"/> object representing the initialized setup progress.</returns>
        /// <exception cref="Exception">Throws if the API call fails.</exception>
        public async Task<SetupProgress> InitializeSetupAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("InitializeSetupAsync called.");
            try
            {
                ApiResponse<SetupProgress> response = await _client.PostAsync<ApiResponse<SetupProgress>>("/setup/init", null, cancellationToken);
                _logger.LogSuccess("InitializeSetupAsync succeeded.");
                return response.Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in InitializeSetupAsync: {ex.Message}");
                throw new Exception($"Error in InitializeSetupAsync: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Sets the current step in the setup process.
        /// </summary>
        /// <param name="stepIndex">The zero-based index of the step to set.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>A <see cref="SetupProgress"/> object representing the updated setup progress.</returns>
        /// <exception cref="Exception">Throws if the API call fails.</exception>
        public async Task<SetupProgress> SetStepAsync(int stepIndex, CancellationToken cancellationToken = default)
        {
            _logger.LogTrace($"SetStepAsync called with stepIndex={stepIndex}.");
            try
            {
                var payload = new { stepIndex };
                ApiResponse<SetupProgress> response = await _client.PostAsync<ApiResponse<SetupProgress>>("/setup/setStep", payload, cancellationToken);
                _logger.LogSuccess($"SetStepAsync succeeded for stepIndex={stepIndex}.");
                return response.Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in SetStepAsync: {ex.Message}");
                throw new Exception($"Error in SetStepAsync: {ex.Message}", ex);
            }
        }
    }

    /// <summary>
    /// Represents a generic API response wrapper.
    /// </summary>
    /// <typeparam name="TResult">The type of the result contained in the response.</typeparam>
    public class ApiResponse<TResult>
    {
        /// <summary>
        /// Gets or sets the status code of the API response.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the result payload of the API response.
        /// </summary>
        public TResult Result { get; set; }
    }
}
