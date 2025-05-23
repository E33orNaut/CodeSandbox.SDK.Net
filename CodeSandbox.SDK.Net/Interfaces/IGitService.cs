using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Models;

namespace CodeSandbox.SDK.Net.Interfaces
{
    /// <summary>
    /// Interface for interacting with Git-related API endpoints.
    /// </summary>
    public interface IGitService
    {   
        /// <summary>
        /// Asynchronously gets the current Git status.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that returns a <see cref="GitStatusResult"/> representing the current status of the Git repository.</returns>
        Task<GitStatusResult> GetStatusAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously retrieves the configured Git remotes.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that returns a <see cref="GitRemotesResult"/> containing the list of Git remotes.</returns>
        Task<GitRemotesResult> GetRemotesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously gets the difference between the current branch and the specified target branch.
        /// </summary>
        /// <param name="branch">The target branch to compare against.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that returns a <see cref="GitTargetDiffResult"/> representing the diff details.</returns>
        Task<GitTargetDiffResult> GetTargetDiffAsync(string branch, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously performs a Git pull operation on the specified branch.
        /// </summary>
        /// <param name="branch">The branch to pull from.</param>
        /// <param name="force">Indicates whether to force the pull operation.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous pull operation.</returns>
        Task PostPullAsync(string branch, bool force = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously discards changes on the specified file paths.
        /// </summary>
        /// <param name="paths">An array of file paths to discard changes for.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that returns a list of strings representing the paths that were discarded.</returns>
        Task<List<string>> PostDiscardAsync(string[] paths, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously commits staged changes with the given commit message.
        /// </summary>
        /// <param name="message">The commit message to use.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous commit operation.</returns>
        Task PostCommitAsync(string message, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously adds a new remote repository with the specified URL.
        /// </summary>
        /// <param name="url">The URL of the remote repository to add.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation of adding the remote.</returns>
        Task PostRemoteAddAsync(string url, CancellationToken cancellationToken = default);
    }
}
