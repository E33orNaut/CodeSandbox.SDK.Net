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
        Task<GitStatus> GetStatusAsync(CancellationToken cancellationToken = default);

        Task<GitTargetDiff> GetTargetDiffAsync(CancellationToken cancellationToken = default);

        Task<GitRemotes> GetRemotesAsync(CancellationToken cancellationToken = default);

        Task<GitRemoteParams> GetRemoteParamsAsync(string reference, string path, CancellationToken cancellationToken = default);

        Task<GitDiffStatusResult> GetDiffStatusAsync(string baseRef, string headRef, CancellationToken cancellationToken = default);
    }
}
