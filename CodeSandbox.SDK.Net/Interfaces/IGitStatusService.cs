using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Models;

namespace CodeSandbox.SDK.Net.Interfaces
{
    /// <summary>
    /// Interface for Git status-related operations.
    /// </summary>
    public interface IGitStatusService
    {
        /// <summary>
        /// Asynchronously retrieves the current Git status.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that returns the <see cref="GitStatus"/> representing the current status.</returns>
        Task<GitStatus> GetStatusAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously retrieves the difference information for the target branch.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that returns the <see cref="GitTargetDiff"/> for the target branch.</returns>
        Task<GitTargetDiff> GetTargetDiffAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously retrieves the configured Git remotes.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that returns the <see cref="GitRemotes"/> containing remote information.</returns>
        Task<GitRemotes> GetRemotesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously retrieves parameters of a specific Git remote.
        /// </summary>
        /// <param name="reference">The remote reference name.</param>
        /// <param name="path">The repository path.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that returns <see cref="GitRemoteParams"/> for the specified remote.</returns>
        Task<GitRemoteParams> GetRemoteParamsAsync(string reference, string path, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously retrieves the diff status between two Git references.
        /// </summary>
        /// <param name="baseRef">The base Git reference.</param>
        /// <param name="headRef">The head Git reference.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that returns the <see cref="GitDiffStatusResult"/> representing the diff status.</returns>
        Task<GitDiffStatusResult> GetDiffStatusAsync(string baseRef, string headRef, CancellationToken cancellationToken = default);
    }
}
