using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Interfaces;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models.New.GitModels;
using CodeSandbox.SDK.Net.Services;
using CodeSandbox.SDK.Net.Sockets;
using Microsoft.AspNet.SignalR;

/// <summary>
/// SignalR hub for managing Git operations in the sandbox.
/// Tracks user connections by userId and connectionId.
/// </summary>
public class GitHib : Hub
{
    private static ApiClient client = new ApiClient(ServerContext.ApiKey);
    private static GitService service = new GitService(client);

    private static readonly ConcurrentDictionary<string, ConcurrentBag<string>> UserConnections =
        new ConcurrentDictionary<string, ConcurrentBag<string>>();

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

    private string GetUserId()
    {
        var user = Context.User;
        if (user?.Identity != null && user.Identity.IsAuthenticated)
            return user.Identity.Name;

        var userId = Context.QueryString["userId"];
        return !string.IsNullOrEmpty(userId) ? userId : null;
    }

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
        var response = await service.GetStatusAsync(CancellationToken.None);
        if (response != null) 
        {
            await Clients.Caller.getStatusAsync(response);
        } 
        else 
        {
            await Clients.Caller.sendError("Failed to retrieve status.");
        }
         
    } 

    /// <summary>
    /// Gets the list of Git remotes asynchronously.
    /// </summary>
    public async Task GetRemotesAsync() 
    {
        var response = await service.GetRemotesAsync(CancellationToken.None);
        if (response != null) 
        {
            await Clients.Caller.getRemotesAsync(response);
        } 
        else 
        {
            await Clients.Caller.sendError("Failed to retrieve remote information.");
        }
    }

    /// <summary>
    /// Gets the diff for a target branch asynchronously.
    /// </summary>
    public async Task GetTargetDiffAsync(string branch) 
    {
        var response = await service.GetTargetDiffAsync(branch, CancellationToken.None);
        if (response != null) 
        {
            await Clients.Caller.getTargetDiffAsync(response);
        } 
        else 
        {
            await Clients.Caller.sendError("Failed to retrieve target diff.");
        }
    }

    /// <summary>
    /// Pulls changes from the specified branch asynchronously.
    /// </summary>
    public async Task PostPullAsync(string branch) 
    {
        await service.PostPullAsync(branch, force: false, CancellationToken.None);
        
    }

    /// <summary>
    /// Discards changes for the specified paths asynchronously.
    /// </summary>
    public async Task PostDiscardAsync(string[] paths)        
    {
        var response =  await service.PostDiscardAsync(paths, CancellationToken.None);
        if (response != null)
        {
            await Clients.Caller.postDiscardAsync(response);
        }
        else
        {
            await Clients.Caller.sendError("Failed to retrieve target diff.");
        }
    }

    /// <summary>
    /// Commits changes with the specified message asynchronously.
    /// </summary>
    public async Task PostCommitAsync(string message)
    {
        var response = await service.PostCommitAsync(message, CancellationToken.None);
        if (response != null)
        {
            await Clients.Caller.postCommitAsync(response);
        }
        else
        {
            await Clients.Caller.sendError("Failed to commit changes.");
        }
    }

    /// <summary>
    /// Adds a new remote asynchronously.
    /// </summary>
    public async Task PostRemoteAddAsync(string url)
    {
        await service.PostRemoteAddAsync(url, CancellationToken.None); 
    }

    /// <summary>
    /// Pushes changes to the default remote asynchronously.
    /// </summary>
    public async Task PostPushAsync()
    {
        await service.PostPushAsync(CancellationToken.None);
        
    }

    /// <summary>
    /// Pushes changes to a specific remote and branch asynchronously.
    /// </summary>
    public async Task PostPushToRemoteAsync(string url, string branch, bool squashAllCommits = false)
    {
        await service.PostPushToRemoteAsync(url, branch, squashAllCommits, CancellationToken.None);
    }

    /// <summary>
    /// Renames a branch asynchronously.
    /// </summary>
    public async Task PostRenameBranchAsync(string oldBranch, string newBranch)
    {
        await service.PostRenameBranchAsync(oldBranch, newBranch, CancellationToken.None);
    }

    /// <summary>
    /// Gets the content of a remote file asynchronously.
    /// </summary>
    public async Task PostRemoteContentAsync(string refrence, string path)

    {
        var response = await service.PostRemoteContentAsync(refrence, path, CancellationToken.None);
        if (response != null)
        {
            await Clients.Caller.postRemoteContentAsync(response);
        }
        else
        {
            await Clients.Caller.sendError("Failed to retrieve remote content.");
        }
    }

    /// <summary>
    /// Gets the diff status between two references asynchronously.
    /// </summary>
    public async Task PostDiffStatusAsync(string baseRef, string headRef)
    {
        var response = await service.PostDiffStatusAsync(baseRef, headRef, CancellationToken.None);
        if (response != null)
        {
            await Clients.Caller.postDiffStatusAsync(response);
        }
        else
        {
            await Clients.Caller.sendError("Failed to retrieve diff status.");
        }
    }

    /// <summary>
    /// Resets the local branch with the remote asynchronously.
    /// </summary>
    public async Task PostResetLocalWithRemoteAsync()
    {
               await service.PostResetLocalWithRemoteAsync(CancellationToken.None);
    }

    /// <summary>
    /// Checks out the initial branch asynchronously.
    /// </summary>
    public async Task PostCheckoutInitialBranchAsync()
    {
               await service.PostCheckoutInitialBranchAsync(CancellationToken.None);
    }

    /// <summary>
    /// Transposes lines in a file asynchronously.
    /// </summary>
    public async Task PostTransposeLinesAsync(List<GitTransposeLinesResultItem> requests)
    {         
        var response = await service.PostTransposeLinesAsync(requests, CancellationToken.None);
        if (response != null)
        {
            await Clients.Caller.postTransposeLinesAsync(response);
        }
        else
        {
            await Clients.Caller.sendError("Failed to transpose lines.");
        }
    }

}
