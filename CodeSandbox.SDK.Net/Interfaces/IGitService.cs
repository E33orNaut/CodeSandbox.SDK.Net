using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Models.New.GitModels;

namespace CodeSandbox.SDK.Net.Interfaces
{
    /// <summary>
    /// Interface for interacting with Git-related API endpoints.
    /// </summary>
    public interface IGitService
    {
        Task<GitStatusResponse> GetStatusAsync(CancellationToken cancellationToken = default);
        Task<GitRemotesResponse> GetRemotesAsync(CancellationToken cancellationToken = default);
        Task<GitTargetDiffResponse> GetTargetDiffAsync(string branch, CancellationToken cancellationToken = default);
        Task PostPullAsync(string branch, bool force = false, CancellationToken cancellationToken = default);
        Task<List<string>> PostDiscardAsync(string[] paths, CancellationToken cancellationToken = default);
        Task<string> PostCommitAsync(string message, CancellationToken cancellationToken = default);
        Task PostRemoteAddAsync(string url, CancellationToken cancellationToken = default);
        Task PostPushAsync(CancellationToken cancellationToken = default);
        Task PostPushToRemoteAsync(string url, string branch, bool squashAllCommits = false, CancellationToken cancellationToken = default);
        Task PostRenameBranchAsync(string oldBranch, string newBranch, CancellationToken cancellationToken = default);
        Task<string> PostRemoteContentAsync(string reference, string path, CancellationToken cancellationToken = default);
        Task<GitDiffStatusResponse> PostDiffStatusAsync(string baseRef, string headRef, CancellationToken cancellationToken = default);
        Task PostResetLocalWithRemoteAsync(CancellationToken cancellationToken = default);
        Task PostCheckoutInitialBranchAsync(CancellationToken cancellationToken = default);
        Task<List<GitTransposeLinesResultItem>> PostTransposeLinesAsync(List<GitTransposeLinesResultItem> requests, CancellationToken cancellationToken = default);
    }
}