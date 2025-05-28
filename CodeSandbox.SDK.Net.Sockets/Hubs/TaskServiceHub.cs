using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Interfaces;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models.New.SandboxTaskModels;
using CodeSandbox.SDK.Net.Services;
using Microsoft.AspNet.SignalR;

namespace CodeSandbox.SDK.Net.Sockets.Hubs
{
    /// <summary>
    /// SignalR hub for managing sandbox task operations.
    /// Tracks user connections by userId and connectionId.
    /// </summary>
    public class TaskServiceHub : Hub
    {
        private static readonly IApiClient client = new ApiClient(ServerContext.ApiKey);
        private static readonly ITaskService service = new TaskService(client, new LoggerService());

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
        /// Starts a new sandbox task asynchronously.
        /// </summary>
        public async Task<object> StartTaskAsync(SandboxTaskStartRequest request)
        {
            try
            {
                // Assuming StartTaskAsync is a method on ITaskService/TaskService
                var result = await service.RunTaskAsync(request.TaskId);
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        /// <summary>
        /// Gets the status of a sandbox task asynchronously.
        /// </summary>
        public async Task<object> GetTaskStatusAsync(string taskId)
        {
            try
            {
                // Assuming GetTaskStatusAsync is a method on ITaskService/TaskService
                var result = await service.GetTaskListAsync();
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        /// <summary>
        /// Cancels a sandbox task asynchronously.
        /// </summary>
        public async Task<object> CancelTaskAsync(string taskId)
        {
            try
            {
                // Assuming StopTaskAsync is a method on ITaskService/TaskService
                var result = await service.StopTaskAsync(taskId);
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        /// <summary>
        /// Lists all tasks for the current user asynchronously.
        /// </summary>
        public async Task<object> ListTasksAsync()
        {
            try
            {
                var result = await service.GetTaskListAsync();
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }
    }
}