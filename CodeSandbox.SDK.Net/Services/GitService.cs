using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Models.New.GitModels;
using CodeSandbox.SDK.Net.Interfaces;
using CodeSandbox.SDK.Net.Internal;
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
        /// Initializes a new instance of the <see cref="GitService"/> class.
        /// </summary>
        /// <param name="client">The API client instance (required).</param>
        /// <param name="logger">Optional logger instance.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="client"/> is null.</exception>
        public GitService(ApiClient client, LoggerService logger = null)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? new LoggerService(LogLevel.Trace);
        }

        /// <summary>
        /// Gets the current Git status.
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>
        /// A <see cref="GitStatusResponse"/> containing the status code and a <see cref="GitStatusResult"/> with the current Git status.
        /// </returns>
        /// <exception cref="Exception">Thrown when the API call fails or an unexpected error occurs.</exception>
        public async Task<GitStatusResponse> GetStatusAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting GetStatusAsync...");
            try
            {
                GitStatusResponse response = await _client.PostAsync<GitStatusResponse>("/git/status", new { }, cancellationToken);
                _logger.LogSuccess("GetStatusAsync completed successfully.");
                return response;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in GetStatusAsync: {ex.Message} (Status: {ex.StatusCode})");
#if DEBUG
                _logger.LogTrace($"DEBUG - API Response Content: {ex.Message}");
#endif
                throw new Exception($"API error while getting Git status: {ex.Message} (Status: {ex.StatusCode})", ex);
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

        /// <summary>
        /// Gets the list of Git remotes.
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>
        /// A <see cref="GitRemotesResponse"/> containing the status code and a <see cref="GitRemotesResult"/> with the list of remotes.
        /// </returns>
        /// <exception cref="Exception">Thrown when the API call fails or an unexpected error occurs.</exception>
        public async Task<GitRemotesResponse> GetRemotesAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting GetRemotesAsync...");
            try
            {
                var response = await _client.PostAsync<GitRemotesResponse>("/git/remotes", new { }, cancellationToken);
                _logger.LogSuccess("GetRemotesAsync completed successfully.");
                return response;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in GetRemotesAsync: {ex.Message} (Status: {ex.StatusCode})");
#if DEBUG
                _logger.LogTrace($"DEBUG - API Response Content: {ex.Message}");
#endif
                throw new Exception($"API error while getting Git remotes: {ex.Message} (Status: {ex.StatusCode})", ex);
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

        /// <summary>
        /// Gets the diff of the target branch.
        /// </summary>
        /// <param name="branch">The target branch name.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>
        /// A <see cref="GitTargetDiffResponse"/> containing the status code and a <see cref="GitTargetDiffResult"/> with the diff details.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="branch"/> is null or whitespace.</exception>
        /// <exception cref="Exception">Thrown when the API call fails or an unexpected error occurs.</exception>
        public async Task<GitTargetDiffResponse> GetTargetDiffAsync(string branch, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(branch))
            {
                throw new ArgumentException("Branch name cannot be null or whitespace.", nameof(branch));
            }

            _logger.LogInfo($"Starting GetTargetDiffAsync for branch '{branch}'...");
            try
            {
                var response = await _client.PostAsync<GitTargetDiffResponse>("/git/targetDiff", new { branch }, cancellationToken);
                _logger.LogSuccess($"GetTargetDiffAsync for branch '{branch}' completed successfully.");
                return response;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in GetTargetDiffAsync for branch '{branch}': {ex.Message} (Status: {ex.StatusCode})");
#if DEBUG
                _logger.LogTrace($"DEBUG - API Response Content: {ex.Message}");
#endif
                throw new Exception($"API error while getting target diff for branch '{branch}': {ex.Message} (Status: {ex.StatusCode})", ex);
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

        /// <summary>
        /// Pulls changes from the specified branch.
        /// </summary>
        /// <param name="branch">The branch name to pull from.</param>
        /// <param name="force">If true, forces the pull operation.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="branch"/> is null or whitespace.</exception>
        /// <exception cref="Exception">Thrown when the API call fails or an unexpected error occurs.</exception>
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
                _logger.LogError($"API error in PostPullAsync for branch '{branch}': {ex.Message} (Status: {ex.StatusCode})");
#if DEBUG
                _logger.LogTrace($"DEBUG - API Response Content: {ex.Message}");
#endif
                throw new Exception($"API error while pulling branch '{branch}': {ex.Message} (Status: {ex.StatusCode})", ex);
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

        /// <summary>
        /// Discards changes in the specified file paths.
        /// </summary>
        /// <param name="paths">Array of file paths to discard changes for.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>
        /// A list of discarded file paths.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="paths"/> is null.</exception>
        /// <exception cref="Exception">Thrown when the API call fails or an unexpected error occurs.</exception>
        public async Task<List<string>> PostDiscardAsync(string[] paths, CancellationToken cancellationToken = default)
        {
            if (paths == null)
            {
                throw new ArgumentNullException(nameof(paths));
            }

            _logger.LogInfo($"Starting PostDiscardAsync for {paths.Length} paths...");
            try
            {
                GitDiscardResponse discardResult = await _client.PostAsync<GitDiscardResponse>("/git/discard", new { paths }, cancellationToken);
                _logger.LogSuccess("PostDiscardAsync completed successfully.");
                return discardResult?.Result?.Paths;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in PostDiscardAsync: {ex.Message} (Status: {ex.StatusCode})");
#if DEBUG
                _logger.LogTrace($"DEBUG - API Response Content: {ex.Message}");
#endif
                throw new Exception($"API error while discarding paths: {ex.Message} (Status: {ex.StatusCode})", ex);
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

        /// <summary>
        /// Creates a commit with the specified message.
        /// </summary>
        /// <param name="message">The commit message.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>
        /// The shell ID of the created commit.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="message"/> is null or whitespace.</exception>
        /// <exception cref="Exception">Thrown when the API call fails or an unexpected error occurs.</exception>
        public async Task<string> PostCommitAsync(string message, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Commit message cannot be null or whitespace.", nameof(message));

            _logger.LogInfo($"Starting PostCommitAsync with message '{message}'...");
            try
            {
                var response = await _client.PostAsync<GitCommitResponse>("/git/commit", new { message }, cancellationToken);
                _logger.LogSuccess("PostCommitAsync completed successfully.");
                return response?.Result?.ShellId;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in PostCommitAsync: {ex.Message} (Status: {ex.StatusCode})");
                throw new Exception($"API error while committing: {ex.Message} (Status: {ex.StatusCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in PostCommitAsync.", ex);
                throw new Exception($"Unexpected error while committing: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Adds a new remote with the specified URL.
        /// </summary>
        /// <param name="url">The remote URL to add.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="url"/> is null or whitespace.</exception>
        /// <exception cref="Exception">Thrown when the API call fails or an unexpected error occurs.</exception>
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
                _logger.LogError($"API error in PostRemoteAddAsync: {ex.Message} (Status: {ex.StatusCode})");
#if DEBUG
                _logger.LogTrace($"DEBUG - API Response Content: {ex.Message}");
#endif
                throw new Exception($"API error while adding remote: {ex.Message} (Status: {ex.StatusCode})", ex);
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

        /// <summary>
        /// Pushes changes to the remote repository.
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the API call fails or an unexpected error occurs.</exception>
        public async Task PostPushAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting PostPushAsync...");
            try
            {
                await _client.PostAsync<object>("/git/push", new { }, cancellationToken);
                _logger.LogSuccess("PostPushAsync completed successfully.");
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in PostPushAsync: {ex.Message} (Status: {ex.StatusCode})");
                throw new Exception($"API error while pushing: {ex.Message} (Status: {ex.StatusCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in PostPushAsync.", ex);
                throw new Exception($"Unexpected error while pushing: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Pushes changes to a specific remote and branch.
        /// </summary>
        /// <param name="url">The remote URL.</param>
        /// <param name="branch">The branch to push to.</param>
        /// <param name="squashAllCommits">If true, squashes all commits before pushing.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="url"/> or <paramref name="branch"/> is null or whitespace.</exception>
        /// <exception cref="Exception">Thrown when the API call fails or an unexpected error occurs.</exception>
        public async Task PostPushToRemoteAsync(string url, string branch, bool squashAllCommits = false, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentException("URL cannot be null or whitespace.", nameof(url));
            if (string.IsNullOrWhiteSpace(branch)) throw new ArgumentException("Branch cannot be null or whitespace.", nameof(branch));

            _logger.LogInfo($"Starting PostPushToRemoteAsync to {url} branch {branch}...");
            try
            {
                await _client.PostAsync<object>("/git/pushToRemote", new { url, branch, squashAllCommits }, cancellationToken);
                _logger.LogSuccess("PostPushToRemoteAsync completed successfully.");
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in PostPushToRemoteAsync: {ex.Message} (Status: {ex.StatusCode})");
                throw new Exception($"API error while pushing to remote: {ex.Message} (Status: {ex.StatusCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in PostPushToRemoteAsync.", ex);
                throw new Exception($"Unexpected error while pushing to remote: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Renames a branch in the local repository.
        /// </summary>
        /// <param name="oldBranch">The current branch name.</param>
        /// <param name="newBranch">The new branch name.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="oldBranch"/> or <paramref name="newBranch"/> is null or whitespace.</exception>
        /// <exception cref="Exception">Thrown when the API call fails or an unexpected error occurs.</exception>
        public async Task PostRenameBranchAsync(string oldBranch, string newBranch, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(oldBranch)) throw new ArgumentException("Old branch cannot be null or whitespace.", nameof(oldBranch));
            if (string.IsNullOrWhiteSpace(newBranch)) throw new ArgumentException("New branch cannot be null or whitespace.", nameof(newBranch));

            _logger.LogInfo($"Starting PostRenameBranchAsync from {oldBranch} to {newBranch}...");
            try
            {
                await _client.PostAsync<object>("/git/renameBranch", new { oldBranch, newBranch }, cancellationToken);
                _logger.LogSuccess("PostRenameBranchAsync completed successfully.");
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in PostRenameBranchAsync: {ex.Message} (Status: {ex.StatusCode})");
                throw new Exception($"API error while renaming branch: {ex.Message} (Status: {ex.StatusCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in PostRenameBranchAsync.", ex);
                throw new Exception($"Unexpected error while renaming branch: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Retrieves the content of a file from a remote branch or commit.
        /// </summary>
        /// <param name="reference">The branch or commit reference.</param>
        /// <param name="path">The file path to retrieve content from.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>
        /// The file content as a string.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="reference"/> or <paramref name="path"/> is null or whitespace.</exception>
        /// <exception cref="Exception">Thrown when the API call fails or an unexpected error occurs.</exception>
        public async Task<string> PostRemoteContentAsync(string reference, string path, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(reference)) throw new ArgumentException("Reference cannot be null or whitespace.", nameof(reference));
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException("Path cannot be null or whitespace.", nameof(path));

            _logger.LogInfo($"Starting PostRemoteContentAsync for reference {reference} and path {path}...");
            try
            {
                var response = await _client.PostAsync<GitRemoteContentResponse>("/git/remoteContent", new { reference, path }, cancellationToken);
                _logger.LogSuccess("PostRemoteContentAsync completed successfully.");
                return response?.Result?.Content;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in PostRemoteContentAsync: {ex.Message} (Status: {ex.StatusCode})");
                throw new Exception($"API error while getting remote content: {ex.Message} (Status: {ex.StatusCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in PostRemoteContentAsync.", ex);
                throw new Exception($"Unexpected error while getting remote content: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Retrieves the status of changes between two references.
        /// </summary>
        /// <param name="baseRef">The base reference (branch or commit).</param>
        /// <param name="headRef">The head reference (branch or commit).</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>
        /// A <see cref="GitDiffStatusResponse"/> containing the status code and a <see cref="GitDiffStatusResult"/> with the diff status.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="baseRef"/> or <paramref name="headRef"/> is null or whitespace.</exception>
        /// <exception cref="Exception">Thrown when the API call fails or an unexpected error occurs.</exception>
        public async Task<GitDiffStatusResponse> PostDiffStatusAsync(string baseRef, string headRef, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(baseRef)) throw new ArgumentException("Base reference cannot be null or whitespace.", nameof(baseRef));
            if (string.IsNullOrWhiteSpace(headRef)) throw new ArgumentException("Head reference cannot be null or whitespace.", nameof(headRef));

            _logger.LogInfo($"Starting PostDiffStatusAsync from {baseRef} to {headRef}...");
            try
            {
                var response = await _client.PostAsync<GitDiffStatusResponse>("/git/diffStatus", new { @base = baseRef, head = headRef }, cancellationToken);
                _logger.LogSuccess("PostDiffStatusAsync completed successfully.");
                return response;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in PostDiffStatusAsync: {ex.Message} (Status: {ex.StatusCode})");
                throw new Exception($"API error while getting diff status: {ex.Message} (Status: {ex.StatusCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in PostDiffStatusAsync.", ex);
                throw new Exception($"Unexpected error while getting diff status: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Resets the local repository to match the remote.
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the API call fails or an unexpected error occurs.</exception>
        public async Task PostResetLocalWithRemoteAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting PostResetLocalWithRemoteAsync...");
            try
            {
                await _client.PostAsync<object>("/git/resetLocalWithRemote", new { }, cancellationToken);
                _logger.LogSuccess("PostResetLocalWithRemoteAsync completed successfully.");
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in PostResetLocalWithRemoteAsync: {ex.Message} (Status: {ex.StatusCode})");
                throw new Exception($"API error while resetting local with remote: {ex.Message} (Status: {ex.StatusCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in PostResetLocalWithRemoteAsync.", ex);
                throw new Exception($"Unexpected error while resetting local with remote: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Checks out the initial branch of the repository.
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when the API call fails or an unexpected error occurs.</exception>
        public async Task PostCheckoutInitialBranchAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting PostCheckoutInitialBranchAsync...");
            try
            {
                await _client.PostAsync<object>("/git/checkoutInitialBranch", new { }, cancellationToken);
                _logger.LogSuccess("PostCheckoutInitialBranchAsync completed successfully.");
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in PostCheckoutInitialBranchAsync: {ex.Message} (Status: {ex.StatusCode})");
                throw new Exception($"API error while checking out initial branch: {ex.Message} (Status: {ex.StatusCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in PostCheckoutInitialBranchAsync.", ex);
                throw new Exception($"Unexpected error while checking out initial branch: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Maps line numbers between different git commits.
        /// </summary>
        /// <param name="requests">A list of <see cref="GitTransposeLinesResultItem"/> specifying the files and lines to transpose.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>
        /// A list of <see cref="GitTransposeLinesResultItem"/> with the mapped line numbers.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="requests"/> is null or empty.</exception>
        /// <exception cref="Exception">Thrown when the API call fails or an unexpected error occurs.</exception>
        public async Task<List<GitTransposeLinesResultItem>> PostTransposeLinesAsync(List<GitTransposeLinesResultItem> requests, CancellationToken cancellationToken = default)
        {
            if (requests == null || requests.Count == 0)
                throw new ArgumentException("Requests cannot be null or empty.", nameof(requests));

            _logger.LogInfo("Starting PostTransposeLinesAsync...");
            try
            {
                var response = await _client.PostAsync<GitTransposeLinesResponse>("/git/transposeLines", requests, cancellationToken);
                _logger.LogSuccess("PostTransposeLinesAsync completed successfully.");
                return response?.Result;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in PostTransposeLinesAsync: {ex.Message} (Status: {ex.StatusCode})");
                throw new Exception($"API error while transposing lines: {ex.Message} (Status: {ex.StatusCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in PostTransposeLinesAsync.", ex);
                throw new Exception($"Unexpected error while transposing lines: {ex.Message}", ex);
            }
        }
    }
}