using System;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Interfaces;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models;

namespace CodeSandbox.SDK.Net.Services
{
    /// <summary>
    /// Service for retrieving Git status and related information.
    /// </summary>
    public class GitStatusService : IGitStatusService
    {
        private readonly ApiClient _client;
        private readonly LoggerService _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="GitStatusService"/>.
        /// </summary>
        /// <param name="client">API client instance (required).</param>
        /// <param name="logger">Optional logger instance.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="client"/> is null.</exception>
        public GitStatusService(ApiClient client, LoggerService logger = null)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? new LoggerService(LogLevel.Trace);
        }

        /// <inheritdoc/>
        public async Task<GitStatus> GetStatusAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting GetStatusAsync...");
            try
            {
                GitStatus result = await _client.GetAsync<GitStatus>("/git/status", cancellationToken);
                _logger.LogSuccess("GetStatusAsync completed successfully.");
                return result;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in GetStatusAsync: {ex.Message} (Status: {ex.ErrorCode})");
#if DEBUG
                _logger.LogTrace($"DEBUG - API Response Content: {ex.Message}");
#endif
                throw new Exception($"API error while getting Git status: {ex.Message} (Status: {ex.ErrorCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in GetStatusAsync.", ex);
#if DEBUG
                _logger.LogTrace($"DEBUG - Exception Details: {ex.GetType().Name}: {ex.Message}\n{ex.StackTrace}");
#endif
                throw new Exception($"Unexpected error while getting Git status: {ex.Message}", ex);
            }
        }

        /// <inheritdoc/>
        public async Task<GitTargetDiff> GetTargetDiffAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting GetTargetDiffAsync...");
            try
            {
                GitTargetDiff result = await _client.GetAsync<GitTargetDiff>("/git/targetDiff", cancellationToken);
                _logger.LogSuccess("GetTargetDiffAsync completed successfully.");
                return result;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in GetTargetDiffAsync: {ex.Message} (Status: {ex.ErrorCode})");
#if DEBUG
                _logger.LogTrace($"DEBUG - API Response Content: {ex.Message}");
#endif
                throw new Exception($"API error while getting Git target diff: {ex.Message} (Status: {ex.ErrorCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in GetTargetDiffAsync.", ex);
#if DEBUG
                _logger.LogTrace($"DEBUG - Exception Details: {ex.GetType().Name}: {ex.Message}\n{ex.StackTrace}");
#endif
                throw new Exception($"Unexpected error while getting Git target diff: {ex.Message}", ex);
            }
        }

        /// <inheritdoc/>
        public async Task<GitRemotes> GetRemotesAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting GetRemotesAsync...");
            try
            {
                GitRemotes result = await _client.GetAsync<GitRemotes>("/git/remotes", cancellationToken);
                _logger.LogSuccess("GetRemotesAsync completed successfully.");
                return result;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in GetRemotesAsync: {ex.Message} (Status: {ex.ErrorCode})");
#if DEBUG
                _logger.LogTrace($"DEBUG - API Response Content: {ex.Message}");
#endif
                throw new Exception($"API error while getting Git remotes: {ex.Message} (Status: {ex.ErrorCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in GetRemotesAsync.", ex);
#if DEBUG
                _logger.LogTrace($"DEBUG - Exception Details: {ex.GetType().Name}: {ex.Message}\n{ex.StackTrace}");
#endif
                throw new Exception($"Unexpected error while getting Git remotes: {ex.Message}", ex);
            }
        }

        /// <inheritdoc/>
        public async Task<GitRemoteParams> GetRemoteParamsAsync(string reference, string path, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(reference))
            {
                throw new ArgumentNullException(nameof(reference), "Reference cannot be null or empty.");
            }

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path), "Path cannot be null or empty.");
            }

            _logger.LogInfo($"Starting GetRemoteParamsAsync for reference '{reference}', path '{path}'...");

            try
            {
                string url = $"/git/remoteParams?reference={Uri.EscapeDataString(reference)}&path={Uri.EscapeDataString(path)}";
                GitRemoteParams result = await _client.GetAsync<GitRemoteParams>(url, cancellationToken);
                _logger.LogSuccess("GetRemoteParamsAsync completed successfully.");
                return result;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in GetRemoteParamsAsync for reference '{reference}', path '{path}': {ex.Message} (Status: {ex.ErrorCode})");
#if DEBUG
                _logger.LogTrace($"DEBUG - API Response Content: {ex.Message}");
#endif
                throw new Exception($"API error while getting remote params for reference '{reference}', path '{path}': {ex.Message} (Status: {ex.ErrorCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in GetRemoteParamsAsync for reference '{reference}', path '{path}'.", ex);
#if DEBUG
                _logger.LogTrace($"DEBUG - Exception Details: {ex.GetType().Name}: {ex.Message}\n{ex.StackTrace}");
#endif
                throw new Exception($"Unexpected error while getting remote params for reference '{reference}', path '{path}': {ex.Message}", ex);
            }
        }

        /// <inheritdoc/>
        public async Task<GitDiffStatusResult> GetDiffStatusAsync(string baseRef, string headRef, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(baseRef))
            {
                throw new ArgumentNullException(nameof(baseRef), "Base reference cannot be null or empty.");
            }

            if (string.IsNullOrWhiteSpace(headRef))
            {
                throw new ArgumentNullException(nameof(headRef), "Head reference cannot be null or empty.");
            }

            _logger.LogInfo($"Starting GetDiffStatusAsync between '{baseRef}' and '{headRef}'...");

            try
            {
                GitDiffStatusParams parameters = new GitDiffStatusParams
                {
                    Base = baseRef,
                    Head = headRef
                };
                GitDiffStatusResult result = await _client.PostAsync<GitDiffStatusResult>("/git/diffStatus", parameters, cancellationToken);
                _logger.LogSuccess("GetDiffStatusAsync completed successfully.");
                return result;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in GetDiffStatusAsync: {ex.Message} (Status: {ex.ErrorCode})");
#if DEBUG
                _logger.LogTrace($"DEBUG - API Response Content: {ex.Message}");
#endif
                throw new Exception($"API error while getting diff status between '{baseRef}' and '{headRef}': {ex.Message} (Status: {ex.ErrorCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in GetDiffStatusAsync.", ex);
#if DEBUG
                _logger.LogTrace($"DEBUG - Exception Details: {ex.GetType().Name}: {ex.Message}\n{ex.StackTrace}");
#endif
                throw new Exception($"Unexpected error while getting diff status between '{baseRef}' and '{headRef}': {ex.Message}", ex);
            }
        }
    }
}
