using System;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models.New.SandboxTaskModels;
using CodeSandbox.SDK.Net.Services;
using Microsoft.AspNet.SignalR;

namespace CodeSandbox.SDK.Net.Sockets.Hubs
{
    public class TaskServiceHub : Hub
    {
        private static readonly ApiClient client = new ApiClient(ServerContext.ApiKey);
        private static readonly TaskService service = new TaskService(client.HttpClient, new LoggerService(LogLevel.Trace));

    }
}