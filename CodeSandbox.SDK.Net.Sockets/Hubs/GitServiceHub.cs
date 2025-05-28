using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Interfaces;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models.New.GitModels;
using CodeSandbox.SDK.Net.Services;
using CodeSandbox.SDK.Net.Sockets;
using Microsoft.AspNet.SignalR;

namespace CodeSandbox.SDK.Net.Sockets.Hubs
{

    /// <summary>
    /// SignalR hub for managing Git operations in the sandbox.
    /// Tracks user connections by userId and connectionId.
    /// </summary>
    public class GitServiceHub : Hub
    {
        private static readonly IApiClient client = new ApiClient(ServerContext.ApiKey);
        private static readonly GitService service = new GitService(client);

        private static readonly ConcurrentDictionary<string, ConcurrentBag<string>> UserConnections =
            new ConcurrentDictionary<string, ConcurrentBag<string>>();

        /// <inheritdoc />
        public override Task OnConnected()
        {
            string userId = GetUserId();
            string connectionId = Context.ConnectionId;

            if (!string.IsNullOrEmpty(userId))
            {
                var connections = UserConnections.GetOrAdd(userId, _ => new ConcurrentBag<string>());
                connections.Add(connectionId);
            }

            return base.OnConnected();
        }

        /// <inheritdoc />
        public override Task OnDisconnected(bool stopCalled)
        {
            string userId = GetUserId();
            string connectionId = Context.ConnectionId;

            if (!string.IsNullOrEmpty(userId) && UserConnections.TryGetValue(userId, out var connections))
            {
                var updated = new ConcurrentBag<string>();
                foreach (var id in connections)
                {
                    if (id != connectionId)
                        updated.Add(id);
                }
                if (!updated.IsEmpty)
                    UserConnections[userId] = updated;
                else
                    UserConnections.TryRemove(userId, out _);
            }

            return base.OnDisconnected(stopCalled);
        }

        /// <inheritdoc />
        public override Task OnReconnected()
        {
            string userId = GetUserId();
            string connectionId = Context.ConnectionId;

            if (!string.IsNullOrEmpty(userId))
            {
                var connections = UserConnections.GetOrAdd(userId, _ => new ConcurrentBag<string>());
                if (!connections.Contains(connectionId))
                    connections.Add(connectionId);
            }

            return base.OnReconnected();
        }

        /// <summary>
        /// Gets the userId for the current connection.
        /// </summary>
        private string GetUserId()
        {
            var user = Context.User;
            if (user?.Identity != null && user.Identity.IsAuthenticated)
                return user.Identity.Name;

            var userId = Context.QueryString["userId"];
            return !string.IsNullOrEmpty(userId) ? userId : null;
        }

        /// <summary>
        /// Gets all connectionIds for a given userId.
        /// </summary>
        public static string[] GetConnectionsForUser(string userId)
        {
            if (UserConnections.TryGetValue(userId, out var connections))
                return connections.ToArray();
            return Array.Empty<string>();
        }

        /// <summary>
        /// Gets the current Git status asynchronously.
        /// </summary>
        public async Task GetStatusAsync()
        {
            try
            {
                var response = await service.GetStatusAsync(CancellationToken.None);
                await Clients.Caller.getStatusAsync(response);
            }
            catch (Exception ex)
            {
                await Clients.Caller.sendError(ex.Message ?? "Failed to retrieve status.");
            }
        }

        /// <summary>
        /// Gets the list of Git remotes asynchronously.
        /// </summary>
        public async Task GetRemotesAsync()
        {
            try
            {
                var response = await service.GetRemotesAsync(CancellationToken.None);
                await Clients.Caller.getRemotesAsync(response);
            }
            catch (Exception ex)
            {
                await Clients.Caller.sendError(ex.Message ?? "Failed to retrieve remote information.");
            }
        }

        /// <summary>
        /// Gets the diff for a target branch asynchronously.
        /// </summary>
        public async Task GetTargetDiffAsync(string branch)
        {
            try
            {
                var response = await service.GetTargetDiffAsync(branch, CancellationToken.None);
                await Clients.Caller.getTargetDiffAsync(response);
            }
            catch (Exception ex)
            {
                await Clients.Caller.sendError(ex.Message ?? "Failed to retrieve target diff.");
            }
        }

        /// <summary>
        /// Pulls changes from the specified branch asynchronously.
        /// </summary>
        public async Task PostPullAsync(string branch)
        {
            try
            {
                await service.PostPullAsync(branch, force: false, CancellationToken.None);
                await Clients.Caller.postPullAsync("Pull completed.");
            }
            catch (Exception ex)
            {
                await Clients.Caller.sendError(ex.Message ?? "Failed to pull changes.");
            }
        }

        /// <summary>
        /// Discards changes for the specified paths asynchronously.
        /// </summary>
        public async Task PostDiscardAsync(string[] paths)
        {
            try
            {
                var response = await service.PostDiscardAsync(paths, CancellationToken.None);
                await Clients.Caller.postDiscardAsync(response);
            }
            catch (Exception ex)
            {
                await Clients.Caller.sendError(ex.Message ?? "Failed to discard changes.");
            }
        }

        /// <summary>
        /// Commits changes with the specified message asynchronously.
        /// </summary>
        public async Task PostCommitAsync(string message)
        {
            try
            {
                var response = await service.PostCommitAsync(message, CancellationToken.None);
                await Clients.Caller.postCommitAsync(response);
            }
            catch (Exception ex)
            {
                await Clients.Caller.sendError(ex.Message ?? "Failed to commit changes.");
            }
        }

        /// <summary>
        /// Adds a new remote asynchronously.
        /// </summary>
        public async Task PostRemoteAddAsync(string url)
        {
            try
            {
                await service.PostRemoteAddAsync(url, CancellationToken.None);
                await Clients.Caller.postRemoteAddAsync("Remote added.");
            }
            catch (Exception ex)
            {
                await Clients.Caller.sendError(ex.Message ?? "Failed to add remote.");
            }
        }

        /// <summary>
        /// Pushes changes to the default remote asynchronously.
        /// </summary>
        public async Task PostPushAsync()
        {
            try
            {
                await service.PostPushAsync(CancellationToken.None);
                await Clients.Caller.postPushAsync("Push completed.");
            }
            catch (Exception ex)
            {
                await Clients.Caller.sendError(ex.Message ?? "Failed to push changes.");
            }
        }

        /// <summary>
        /// Pushes changes to a specific remote and branch asynchronously.
        /// </summary>
        public async Task PostPushToRemoteAsync(string url, string branch, bool squashAllCommits = false)
        {
            try
            {
                await service.PostPushToRemoteAsync(url, branch, squashAllCommits, CancellationToken.None);
                await Clients.Caller.postPushToRemoteAsync("Push to remote completed.");
            }
            catch (Exception ex)
            {
                await Clients.Caller.sendError(ex.Message ?? "Failed to push to remote.");
            }
        }

        /// <summary>
        /// Renames a branch asynchronously.
        /// </summary>
        public async Task PostRenameBranchAsync(string oldBranch, string newBranch)
        {
            try
            {
                await service.PostRenameBranchAsync(oldBranch, newBranch, CancellationToken.None);
                await Clients.Caller.postRenameBranchAsync("Branch renamed.");
            }
            catch (Exception ex)
            {
                await Clients.Caller.sendError(ex.Message ?? "Failed to rename branch.");
            }
        }

        /// <summary>
        /// Gets the content of a remote file asynchronously.
        /// </summary>
        public async Task PostRemoteContentAsync(string reference, string path)
        {
            try
            {
                var response = await service.PostRemoteContentAsync(reference, path, CancellationToken.None);
                await Clients.Caller.postRemoteContentAsync(response);
            }
            catch (Exception ex)
            {
                await Clients.Caller.sendError(ex.Message ?? "Failed to retrieve remote content.");
            }
        }

        /// <summary>
        /// Gets the diff status between two references asynchronously.
        /// </summary>
        public async Task PostDiffStatusAsync(string baseRef, string headRef)
        {
            try
            {
                var response = await service.PostDiffStatusAsync(baseRef, headRef, CancellationToken.None);
                await Clients.Caller.postDiffStatusAsync(response);
            }
            catch (Exception ex)
            {
                await Clients.Caller.sendError(ex.Message ?? "Failed to retrieve diff status.");
            }
        }

        /// <summary>
        /// Resets the local branch with the remote asynchronously.
        /// </summary>
        public async Task PostResetLocalWithRemoteAsync()
        {
            try
            {
                await service.PostResetLocalWithRemoteAsync(CancellationToken.None);
                await Clients.Caller.postResetLocalWithRemoteAsync("Reset completed.");
            }
            catch (Exception ex)
            {
                await Clients.Caller.sendError(ex.Message ?? "Failed to reset local with remote.");
            }
        }

        /// <summary>
        /// Checks out the initial branch asynchronously.
        /// </summary>
        public async Task PostCheckoutInitialBranchAsync()
        {
            try
            {
                await service.PostCheckoutInitialBranchAsync(CancellationToken.None);
                await Clients.Caller.postCheckoutInitialBranchAsync("Checkout completed.");
            }
            catch (Exception ex)
            {
                await Clients.Caller.sendError(ex.Message ?? "Failed to checkout initial branch.");
            }
        }

        /// <summary>
        /// Transposes lines in a file asynchronously.
        /// </summary>
        public async Task PostTransposeLinesAsync(System.Collections.Generic.List<GitTransposeLinesResultItem> requests)
        {
            try
            {
                var response = await service.PostTransposeLinesAsync(requests, CancellationToken.None);
                await Clients.Caller.postTransposeLinesAsync(response);
            }
            catch (Exception ex)
            {
                await Clients.Caller.sendError(ex.Message ?? "Failed to transpose lines.");
            }
        }
    }
}