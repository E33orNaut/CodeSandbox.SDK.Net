using System;
using System.Collections.Concurrent;
using System.Linq;
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
        private static readonly ApiClient client = new ApiClient(ServerContext.ApiKey);
        private static readonly ShellService service = new ShellService(client);

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
        /// Creates a new shell session.
        /// </summary>
        public async Task<object> CreateShell(SandboxShellCreateRequest request)
        {
            try
            {
                SandboxShellSuccessResponse<SandboxOpenShellDTO> result = await service.CreateShellAsync(request);
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
                SandboxShellSuccessResponse<object> result = await service.SendInputAsync(request);
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
                SandboxShellSuccessResponse<SandboxShellListResult> result = await service.ListShellsAsync();
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
                SandboxShellSuccessResponse<SandboxOpenShellDTO> result = await service.OpenShellAsync(request);
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
                SandboxShellSuccessResponse<object> result = await service.CloseShellAsync(request);
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
                SandboxShellSuccessResponse<object> result = await service.RestartShellAsync(request);
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
                SandboxShellSuccessResponse<SandboxShellDTO> result = await service.TerminateShellAsync(request);
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
                SandboxShellSuccessResponse<object> result = await service.ResizeShellAsync(request);
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
                SandboxShellSuccessResponse result = await service.RenameShellAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }
    }
}
