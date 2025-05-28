using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models.New.SandboxContainerModels;
using CodeSandbox.SDK.Net.Services;
using CodeSandbox.SDK.Net.Sockets;
using Microsoft.AspNet.SignalR;

/// <summary>
/// SignalR hub for managing container setup operations.
/// Tracks user connections by userId and connectionId.
/// </summary>
public class ContainerHub : Hub
{
    private static ApiClient client = new ApiClient(ServerContext.ApiKey);
    private static ContainerService service = new ContainerService(client);

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
    /// Sets up a new container asynchronously.
    /// </summary>
    /// <param name="request">The container setup request.</param>
    /// <param name="cancellationToken">Cancellation token (optional).</param>
    public async Task SetupContainerAsync(ContainerSetupRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await service.SetupContainerAsync(request, cancellationToken);
            await Clients.Caller.setupContainerSuccess(result);
        }
        catch (Exception ex)
        {
            await Clients.Caller.setupContainerError(ex.Message ?? "An error occurred during container setup.");
        }
    }
}
