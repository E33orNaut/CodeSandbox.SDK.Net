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
    public class GitService
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

        /// <inheritdoc />
        /// <summary>
        /// Gets the current Git status.
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>A <see cref="GitStatusResult"/> representing the current Git status.</returns>
        /// <exception cref="Exception">Throws when API call fails or unexpected errors occur.</exception>
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

        /// <inheritdoc />
        /// <summary>
        /// Gets the list of Git remotes.
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>A <see cref="GitRemotesResult"/> containing the list of remotes.</returns>
        /// <exception cref="Exception">Throws when API call fails or unexpected errors occur.</exception>
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

        /// <inheritdoc />
        /// <summary>
        /// Gets the diff of the target branch.
        /// </summary>
        /// <param name="branch">The target branch name.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>A <see cref="GitTargetDiffResult"/> containing the diff details.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="branch"/> is null or whitespace.</exception>
        /// <exception cref="Exception">Throws when API call fails or unexpected errors occur.</exception>
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

        /// <inheritdoc />
        /// <summary>
        /// Pulls changes from the specified branch.
        /// </summary>
        /// <param name="branch">The branch name to pull from.</param>
        /// <param name="force">If true, forces the pull operation.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="branch"/> is null or whitespace.</exception>
        /// <exception cref="Exception">Throws when API call fails or unexpected errors occur.</exception>
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

        /// <inheritdoc />
        /// <summary>
        /// Discards changes in the specified file paths.
        /// </summary>
        /// <param name="paths">Array of file paths to discard changes for.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>A list of discarded file paths.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="paths"/> is null.</exception>
        /// <exception cref="Exception">Throws when API call fails or unexpected errors occur.</exception>
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

        /// <inheritdoc />
        /// <summary>
        /// Creates a commit with the specified message.
        /// </summary>
        /// <param name="message">The commit message.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="message"/> is null or whitespace.</exception>
        /// <exception cref="Exception">Throws when API call fails or unexpected errors occur.</exception>
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

        /// <inheritdoc />
        /// <summary>
        /// Adds a new remote with the specified URL.
        /// </summary>
        /// <param name="url">The remote URL to add.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="url"/> is null or whitespace.</exception>
        /// <exception cref="Exception">Throws when API call fails or unexpected errors occur.</exception>
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
    }

    /// <summary>
    /// Represents the result of a Git status request.
    /// </summary>
    public class GitStatusResult
    {
        /// <summary>
        /// Gets or sets the status string.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the list of changes.
        /// </summary>
        [JsonProperty("changes")]
        public List<GitChange> Changes { get; set; }
    }

    /// <summary>
    /// Represents a Git change item.
    /// </summary>
    public class GitChange
    {
        /// <summary>
        /// Gets or sets the path of the changed file.
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the type of change (e.g., modified, added, deleted).
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }

    /// <summary>
    /// Represents the result of a Git remotes request.
    /// </summary>
    public class GitRemotesResult
    {
        /// <summary>
        /// Gets or sets the list of remotes.
        /// </summary>
        [JsonProperty("remotes")]
        public List<GitRemote> Remotes { get; set; }
    }

    /// <summary>
    /// Represents a Git remote repository.
    /// </summary>
    public class GitRemote
    {
        /// <summary>
        /// Gets or sets the name of the remote.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the URL of the remote.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }
    }

    /// <summary>
    /// Represents the result of a Git target diff request.
    /// </summary>
    public class GitTargetDiffResult
    {
        /// <summary>
        /// Gets or sets the branch name.
        /// </summary>
        [JsonProperty("branch")]
        public string Branch { get; set; }

        /// <summary>
        /// Gets or sets the diff details.
        /// </summary>
        [JsonProperty("diff")]
        public string Diff { get; set; }
    }

    /// <summary>
    /// Represents the discard result response from the API.
    /// </summary>
    public class DiscardResult
    {
        /// <summary>
        /// Gets or sets the inner result object.
        /// </summary>
        [JsonProperty("result")]
        public DiscardInnerResult Result { get; set; }
    }

    /// <summary>
    /// Represents the inner discard result details.
    /// </summary>
    public class DiscardInnerResult
    {
        /// <summary>
        /// Gets or sets the list of discarded file paths.
        /// </summary>
        [JsonProperty("paths")]
        public List<string> Paths { get; set; }
    }
}
