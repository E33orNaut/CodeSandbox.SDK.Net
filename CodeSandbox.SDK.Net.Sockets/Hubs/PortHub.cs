using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Services;
using Microsoft.AspNet.SignalR;

namespace CodeSandbox.SDK.Net.Sockets.Hubs
{
    /// <summary>
    /// SignalR hub for managing port operations in the sandbox.
    /// </summary>
    public class PortHub : Hub
    {

        private static ApiClient client = new ApiClient(ServerContext.ApiKey);
        private static PortService service = new PortService(client);

        /// <summary>
        /// Gets the list of available ports asynchronously.
        /// </summary>
        public async Task GetPortListAsync()
        {
            var response = await service.GetPortListAsync(CancellationToken.None);
            if (response != null)
            {
                await Clients.Caller.getPortListAsync(response);
            }
            else
            {
                await Clients.Caller.sendError("Failed to retrieve status.");
            }
        }
    }
}
