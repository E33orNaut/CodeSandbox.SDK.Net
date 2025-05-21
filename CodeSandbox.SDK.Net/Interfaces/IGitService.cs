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
        Task<GitStatusResult> GetStatusAsync(CancellationToken cancellationToken = default);

        Task<GitRemotesResult> GetRemotesAsync(CancellationToken cancellationToken = default);

        Task<GitTargetDiffResult> GetTargetDiffAsync(string branch, CancellationToken cancellationToken = default);

        Task PostPullAsync(string branch, bool force = false, CancellationToken cancellationToken = default);

        Task<List<string>> PostDiscardAsync(string[] paths, CancellationToken cancellationToken = default);

        Task PostCommitAsync(string message, CancellationToken cancellationToken = default);

        Task PostRemoteAddAsync(string url, CancellationToken cancellationToken = default);
    }
}
