using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace CodeSandbox.SDK.Net.Sockets.Hubs
{
    /// <summary>
    /// Internal hub for system service operations.
    /// Tracks user connections by userId and connectionId.
    /// </summary>
    internal class SystemServiceHub : Hub
    {
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
    }
}
