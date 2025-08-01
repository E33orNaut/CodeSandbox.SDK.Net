﻿using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models.New.SandboxContainerModels;
using CodeSandbox.SDK.Net.Services;
using CodeSandbox.SDK.Net.Sockets;
using Microsoft.AspNet.SignalR;
using CodeSandbox.SDK.Net.Interfaces;

namespace CodeSandbox.SDK.Net.Sockets.Hubs
{
    /// <summary>
    /// SignalR hub for managing container setup operations.
    /// Tracks user connections by userId and connectionId.
    /// </summary>
    public class ContainerServiceHub : Hub
    {
        private static readonly IApiClient client = new ApiClient(ServerContext.ApiKey);
        private static readonly ContainerService service = new ContainerService(client);

        private static readonly ConcurrentDictionary<string, ConcurrentBag<string>> UserConnections =
            new ConcurrentDictionary<string, ConcurrentBag<string>>();

        public override Task OnConnected()
        {
            string userId = GetUserId();
            string connectionId = Context.ConnectionId;

            if (!string.IsNullOrEmpty(userId))
            {
                ConcurrentBag<string> connections = UserConnections.GetOrAdd(userId, _ => new ConcurrentBag<string>());
                connections.Add(connectionId);
            }

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string userId = GetUserId();
            string connectionId = Context.ConnectionId;

            if (!string.IsNullOrEmpty(userId) && UserConnections.TryGetValue(userId, out ConcurrentBag<string> connections))
            {
                ConcurrentBag<string> updated = new ConcurrentBag<string>();
                foreach (string id in connections)
                {
                    if (id != connectionId)
                    {
                        updated.Add(id);
                    }
                }
                if (!updated.IsEmpty)
                {
                    UserConnections[userId] = updated;
                }
                else
                {
                    _ = UserConnections.TryRemove(userId, out _);
                }
            }

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            string userId = GetUserId();
            string connectionId = Context.ConnectionId;

            if (!string.IsNullOrEmpty(userId))
            {
                ConcurrentBag<string> connections = UserConnections.GetOrAdd(userId, _ => new ConcurrentBag<string>());
                if (!connections.Contains(connectionId))
                {
                    connections.Add(connectionId);
                }
            }

            return base.OnReconnected();
        }

        private string GetUserId()
        {
            System.Security.Principal.IPrincipal user = Context.User;
            if (user?.Identity != null && user.Identity.IsAuthenticated)
            {
                return user.Identity.Name;
            }

            string userId = Context.QueryString["userId"];
            return !string.IsNullOrEmpty(userId) ? userId : null;
        }

        public static string[] GetConnectionsForUser(string userId)
        {
            return UserConnections.TryGetValue(userId, out ConcurrentBag<string> connections) ? connections.ToArray() : Array.Empty<string>();
        }

        /// <summary>
        /// Sets up a new container asynchronously.
        /// </summary>
        public async Task SetupContainerAsync(ContainerSetupRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                ContainerSetupSuccessResponse result = await service.SetupContainerAsync(request, cancellationToken);
                await Clients.Caller.setupContainerSuccess(result);
            }
            catch (Exception ex)
            {
                await Clients.Caller.setupContainerError(ex.Message ?? "An error occurred during container setup.");
            }
        }

    }
}