using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Interfaces;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models;
using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Services
{
    /// <summary>
    /// Service for interacting with Git-related API endpoints.
    /// </summary>
    public class GitService : IGitService
    {
        private readonly ApiClient _client;
        private readonly LoggerService _logger;

        /// <summary>
        /// Creates a new instance of <see cref="GitService"/>.
        /// </summary>
        /// <param name="client">The API client instance (required).</param>
        /// <param name="logger">Optional logger instance.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="client"/> is null.</exception>
        public GitService(ApiClient client, LoggerService logger = null)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? new LoggerService(LogLevel.Trace);
        }

        public async Task<GitStatusResult> GetStatusAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting GetStatusAsync...");
            try
            {
                GitStatusResult response = await _client.PostAsync<GitStatusResult>("/git/status", new { }, cancellationToken);
                _logger.LogSuccess("GetStatusAsync completed successfully.");
                return response;
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

        public async Task<GitRemotesResult> GetRemotesAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting GetRemotesAsync...");
            try
            {
                GitRemotesResult response = await _client.PostAsync<GitRemotesResult>("/git/remotes", new { }, cancellationToken);
                _logger.LogSuccess("GetRemotesAsync completed successfully.");
                return response;
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

        public async Task<GitTargetDiffResult> GetTargetDiffAsync(string branch, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(branch))
            {
                throw new ArgumentException("Branch name cannot be null or whitespace.", nameof(branch));
            }

            _logger.LogInfo($"Starting GetTargetDiffAsync for branch '{branch}'...");
            try
            {
                GitTargetDiffResult response = await _client.PostAsync<GitTargetDiffResult>("/git/targetDiff", new { branch }, cancellationToken);
                _logger.LogSuccess($"GetTargetDiffAsync for branch '{branch}' completed successfully.");
                return response;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in GetTargetDiffAsync for branch '{branch}': {ex.Message} (Status: {ex.ErrorCode})");
#if DEBUG
                _logger.LogTrace($"DEBUG - API Response Content: {ex.Message}");
#endif
                throw new Exception($"API error while getting target diff for branch '{branch}': {ex.Message} (Status: {ex.ErrorCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in GetTargetDiffAsync for branch '{branch}'.", ex);
#if DEBUG
                _logger.LogTrace($"DEBUG - Exception Details: {ex.GetType().Name}: {ex.Message}\n{ex.StackTrace}");
#endif
                throw new Exception($"Unexpected error while getting target diff for branch '{branch}': {ex.Message}", ex);
            }
        }

        public async Task PostPullAsync(string branch, bool force = false, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(branch))
            {
                throw new ArgumentException("Branch name cannot be null or whitespace.", nameof(branch));
            }

            _logger.LogInfo($"Starting PostPullAsync for branch '{branch}' with force={force}...");
            try
            {
                await _client.PostAsync<object>("/git/pull", new { branch, force }, cancellationToken);
                _logger.LogSuccess($"PostPullAsync for branch '{branch}' completed successfully.");
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in PostPullAsync for branch '{branch}': {ex.Message} (Status: {ex.ErrorCode})");
#if DEBUG
                _logger.LogTrace($"DEBUG - API Response Content: {ex.Message}");
#endif
                throw new Exception($"API error while pulling branch '{branch}': {ex.Message} (Status: {ex.ErrorCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in PostPullAsync for branch '{branch}'.", ex);
#if DEBUG
                _logger.LogTrace($"DEBUG - Exception Details: {ex.GetType().Name}: {ex.Message}\n{ex.StackTrace}");
#endif
                throw new Exception($"Unexpected error while pulling branch '{branch}': {ex.Message}", ex);
            }
        }

        public async Task<List<string>> PostDiscardAsync(string[] paths, CancellationToken cancellationToken = default)
        {
            if (paths == null)
            {
                throw new ArgumentNullException(nameof(paths));
            }

            _logger.LogInfo($"Starting PostDiscardAsync for {paths.Length} paths...");
            try
            {
                DiscardResult discardResult = await _client.PostAsync<DiscardResult>("/git/discard", new { paths }, cancellationToken);
                _logger.LogSuccess("PostDiscardAsync completed successfully.");
                return discardResult?.Result?.Paths;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in PostDiscardAsync: {ex.Message} (Status: {ex.ErrorCode})");
#if DEBUG
                _logger.LogTrace($"DEBUG - API Response Content: {ex.Message}");
#endif
                throw new Exception($"API error while discarding paths: {ex.Message} (Status: {ex.ErrorCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in PostDiscardAsync.", ex);
#if DEBUG
                _logger.LogTrace($"DEBUG - Exception Details: {ex.GetType().Name}: {ex.Message}\n{ex.StackTrace}");
#endif
                throw new Exception($"Unexpected error while discarding paths: {ex.Message}", ex);
            }
        }

        public async Task PostCommitAsync(string message, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Commit message cannot be null or whitespace.", nameof(message));
            }

            _logger.LogInfo($"Starting PostCommitAsync with message '{message}'...");
            try
            {
                await _client.PostAsync<object>("/git/commit", new { message }, cancellationToken);
                _logger.LogSuccess("PostCommitAsync completed successfully.");
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in PostCommitAsync: {ex.Message} (Status: {ex.ErrorCode})");
#if DEBUG
                _logger.LogTrace($"DEBUG - API Response Content: {ex.Message}");
#endif
                throw new Exception($"API error while committing: {ex.Message} (Status: {ex.ErrorCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in PostCommitAsync.", ex);
#if DEBUG
                _logger.LogTrace($"DEBUG - Exception Details: {ex.GetType().Name}: {ex.Message}\n{ex.StackTrace}");
#endif
                throw new Exception($"Unexpected error while committing: {ex.Message}", ex);
            }
        }

        public async Task PostRemoteAddAsync(string url, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("URL cannot be null or whitespace.", nameof(url));
            }

            _logger.LogInfo($"Starting PostRemoteAddAsync with URL '{url}'...");
            try
            {
                await _client.PostAsync<object>("/git/remoteAdd", new { url }, cancellationToken);
                _logger.LogSuccess("PostRemoteAddAsync completed successfully.");
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in PostRemoteAddAsync: {ex.Message} (Status: {ex.ErrorCode})");
#if DEBUG
                _logger.LogTrace($"DEBUG - API Response Content: {ex.Message}");
#endif
                throw new Exception($"API error while adding remote: {ex.Message} (Status: {ex.ErrorCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in PostRemoteAddAsync.", ex);
#if DEBUG
                _logger.LogTrace($"DEBUG - Exception Details: {ex.GetType().Name}: {ex.Message}\n{ex.StackTrace}");
#endif
                throw new Exception($"Unexpected error while adding remote: {ex.Message}", ex);
            }
        }
         
        private class DiscardResult
        {
            [JsonProperty("result")]
            public DiscardPaths Result { get; set; }
        }

        private class DiscardPaths
        {
            [JsonProperty("paths")]
            public List<string> Paths { get; set; }
        }
    }
}
