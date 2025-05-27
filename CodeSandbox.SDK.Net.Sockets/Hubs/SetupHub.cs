using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Services;
using Microsoft.AspNet.SignalR;

namespace CodeSandbox.SDK.Net.Sockets.Hubs
{
    public class SetupHub : Hub
    {

        private static ApiClient client = new ApiClient(ServerContext.ApiKey);
        private static SetupService service = new SetupService(client);



    }
}
