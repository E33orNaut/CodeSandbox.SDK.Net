using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models.New.SandboxShellModels;
using CodeSandbox.SDK.Net.Services;
using Microsoft.AspNet.SignalR;

namespace CodeSandbox.SDK.Net.Sockets.Hubs
{
    /// <summary>
    /// SignalR hub for managing shell operations in the sandbox.
    /// Tracks user connections by userId and connectionId.
    /// </summary>
    public class ShellServiceHub : Hub
    {
        private static ApiClient client = new ApiClient(ServerContext.ApiKey);
        private static ShellService service = new ShellService(client);

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
        /// Creates a new shell session.
        /// </summary>
        public async Task<object> CreateShell(SandboxShellCreateRequest request)
        {
            try
            {
                var result = await service.CreateShellAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        /// <summary>
        /// Sends input to an existing shell session.
        /// </summary>
        public async Task<object> SendInput(SandboxShellInRequest request)
        {
            try
            {
                var result = await service.SendInputAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        /// <summary>
        /// Lists all active shell sessions.
        /// </summary>
        public async Task<object> ListShells()
        {
            try
            {
                var result = await service.ListShellsAsync();
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        /// <summary>
        /// Opens an existing shell session.
        /// </summary>
        public async Task<object> OpenShell(SandboxShellOpenRequest request)
        {
            try
            {
                var result = await service.OpenShellAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        /// <summary>
        /// Closes a shell session.
        /// </summary>
        public async Task<object> CloseShell(SandboxShellIdRequest request)
        {
            try
            {
                var result = await service.CloseShellAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        /// <summary>
        /// Restarts a shell session.
        /// </summary>
        public async Task<object> RestartShell(SandboxShellIdRequest request)
        {
            try
            {
                var result = await service.RestartShellAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        /// <summary>
        /// Terminates a shell session.
        /// </summary>
        public async Task<object> TerminateShell(SandboxShellIdRequest request)
        {
            try
            {
                var result = await service.TerminateShellAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        /// <summary>
        /// Resizes a shell session.
        /// </summary>
        public async Task<object> ResizeShell(SandboxShellResizeRequest request)
        {
            try
            {
                var result = await service.ResizeShellAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        /// <summary>
        /// Renames a shell session.
        /// </summary>
        public async Task<object> RenameShell(SandboxShellRenameRequest request)
        {
            try
            {
                var result = await service.RenameShellAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }
    }
}
