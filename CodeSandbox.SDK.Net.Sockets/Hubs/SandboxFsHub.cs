using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models.New.SandboxFSModels;
using CodeSandbox.SDK.Net.Services;
using Microsoft.AspNet.SignalR;

namespace CodeSandbox.SDK.Net.Sockets.Hubs
{
    /// <summary>
    /// SignalR hub for managing sandbox file system operations.
    /// Tracks user connections by userId and connectionId.
    /// </summary>
    public class SandboxFsHub : Hub
    {
        private static ApiClient client = new ApiClient(ServerContext.ApiKey);
        private static SandboxFsService service = new SandboxFsService(client);

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
        /// Writes a file asynchronously.
        /// </summary>
        public async Task WriteFileAsync(SandboxFSWriteFileRequest request)
        {
            try
            {
                var result = await service.WriteFileAsync(request);
                await Clients.Caller.writeFileSuccess(result);
            }
            catch (Exception ex)
            {
                await Clients.Caller.writeFileError(ex.Message ?? "An error occurred while writing the file.");
            }
        }

        /// <summary>
        /// Searches a file system path asynchronously.
        /// </summary>
        public async Task FsPathSearchAsync(SandboxFSPathSearchResult request)
        {
            try
            {
                var result = await service.FsPathSearchAsync(request);
                await Clients.Caller.fsPathSearchSuccess(result);
            }
            catch (Exception ex)
            {
                await Clients.Caller.fsPathSearchError(ex.Message ?? "An error occurred while searching the file system path.");
            }
        }

        /// <summary>
        /// Uploads a file asynchronously.
        /// </summary>
        public async Task FsUploadAsync(UploadRequest request)
        {
            try
            {
                var result = await service.FsUploadAsync(request);
                await Clients.Caller.fsUploadSuccess(result);
            }
            catch (Exception ex)
            {
                await Clients.Caller.fsUploadError(ex.Message ?? "An error occurred while uploading the file.");
            }
        }

        /// <summary>
        /// Downloads a file asynchronously.
        /// </summary>
        public async Task FsDownloadAsync(DownloadRequest request)
        {
            try
            {
                var result = await service.FsDownloadAsync(request);
                await Clients.Caller.fsDownloadSuccess(result);
            }
            catch (Exception ex)
            {
                await Clients.Caller.fsDownloadError(ex.Message ?? "An error occurred while downloading the file.");
            }
        }

        /// <summary>
        /// Reads a file asynchronously.
        /// </summary>
        public async Task FsReadFileAsync(FSReadFileParams request)
        {
            try
            {
                var result = await service.FsReadFileAsync(request);
                await Clients.Caller.fsReadFileSuccess(result);
            }
            catch (Exception ex)
            {
                await Clients.Caller.fsReadFileError(ex.Message ?? "An error occurred while reading the file.");
            }
        }

        /// <summary>
        /// Reads a directory asynchronously.
        /// </summary>
        public async Task ReadDirAsync(FSReadDirParams request)
        {
            try
            {
                var result = await service.ReadDirAsync(request);
                await Clients.Caller.readDirSuccess(result);
                
            }
            catch (Exception ex)
            {
                await Clients.Caller.readDirError(ex.Message ?? "An error occurred while reading the directory.");
            }
        }

        /// <summary>
        /// Gets file or directory statistics asynchronously.
        /// </summary>
        public async Task StatAsync(FSStatParams request)
        {
            try
            {
                var result = await service.StatAsync(request);
                await Clients.Caller.statSuccess(result);
            }
            catch (Exception ex)
            {
                await Clients.Caller.statError(ex.Message ?? "An error occurred while retrieving file statistics.");
            }
        }

        /// <summary>
        /// Copies a file or directory asynchronously.
        /// </summary>
        public async Task CopyAsync(FSCopyParams request)
        {
            try
            {
                var result = await service.CopyAsync(request);
                await Clients.Caller.copySuccess(result);
            }
            catch (Exception ex)
            {
                await Clients.Caller.copyError(ex.Message ?? "An error occurred while copying the file.");
            }
        }

        /// <summary>
        /// Renames a file or directory asynchronously.
        /// </summary>
        public async Task RenameAsync(FSRenameParams request)
        {
            try
            {
                var result = await service.RenameAsync(request);
                await Clients.Caller.renameSuccess(result);
            }
            catch (Exception ex)
            {
                await Clients.Caller.renameError(ex.Message ?? "An error occurred while renaming the file.");
            }
        }

        /// <summary>
        /// Removes a file or directory asynchronously.
        /// </summary>
        public async Task RemoveAsync(FSRemoveParams request)
        {
            try
            {
                var result = await service.RemoveAsync(request);
                await Clients.Caller.removeSuccess(result);
            }
            catch (Exception ex)
            {
                await Clients.Caller.removeError(ex.Message ?? "An error occurred while removing the file.");
            }
        }
    }
}
