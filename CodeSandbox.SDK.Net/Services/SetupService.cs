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
        /// Initializes a new instance of <see cref="SetupService"/>.
        /// </summary>
        /// <param name="client">API client to send requests.</param>
        /// <param name="logger">Logger service for logging.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="client"/> is null.</exception>
        public SetupService(ApiClient client, LoggerService logger = null)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? new LoggerService(LogLevel.Trace);
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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
    /// Generic API response wrapper.
    /// </summary>
    /// <typeparam name="TResult">Result type.</typeparam>
    public class ApiResponse<TResult>
    {
        /// <inheritdoc/>
        public int Status { get; set; }
        /// <inheritdoc/>
        public TResult Result { get; set; }
    }
}
