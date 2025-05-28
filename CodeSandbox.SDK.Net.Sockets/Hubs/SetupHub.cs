using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Services;
using Microsoft.AspNet.SignalR;

namespace CodeSandbox.SDK.Net.Sockets.Hubs
{
    /// <summary>
    /// SignalR hub for managing setup operations in the sandbox.
    /// Tracks user connections by userId and connectionId.
    /// </summary>
    public class SetupHub : Hub
    {
        private static ApiClient client = new ApiClient(ServerContext.ApiKey);
        private static SetupService service = new SetupService(client);

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
        /// Gets the current setup progress.
        /// </summary>
        public async Task<object> GetSetupProgress()
        {
            try
            {
                var result = await service.GetSetupProgressAsync();
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        /// <summary>
        /// Skips a specific setup step.
        /// </summary>
        public async Task<object> SkipStep(int stepIndexToSkip)
        {
            try
            {
                var result = await service.SkipStepAsync(stepIndexToSkip);
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        /// <summary>
        /// Skips all setup steps.
        /// </summary>
        public async Task<object> SkipAllSteps()
        {
            try
            {
                var result = await service.SkipAllStepsAsync();
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        /// <summary>
        /// Disables the setup process.
        /// </summary>
        public async Task<object> DisableSetup()
        {
            try
            {
                var result = await service.DisableSetupAsync();
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        /// <summary>
        /// Enables the setup process.
        /// </summary>
        public async Task<object> EnableSetup()
        {
            try
            {
                var result = await service.EnableSetupAsync();
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        /// <summary>
        /// Initializes the setup process.
        /// </summary>
        public async Task<object> InitializeSetup()
        {
            try
            {
                var result = await service.InitializeSetupAsync();
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        /// <summary>
        /// Sets the current setup step.
        /// </summary>
        public async Task<object> SetStep(int stepIndex)
        {
            try
            {
                var result = await service.SetStepAsync(stepIndex);
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }
    }
}
