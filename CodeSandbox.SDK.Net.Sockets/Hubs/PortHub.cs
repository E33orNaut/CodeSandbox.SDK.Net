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
    public class PortHub : Hub
    {

        private static ApiClient client = new ApiClient(ServerContext.ApiKey);
        private static PortService service = new PortService(client);

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
