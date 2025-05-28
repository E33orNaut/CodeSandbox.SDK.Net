using System;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models.New.SandboxContainerModels;
using CodeSandbox.SDK.Net.Services;
using CodeSandbox.SDK.Net.Sockets;
using Microsoft.AspNet.SignalR;

/// <summary>
/// SignalR hub for managing container setup operations.
/// </summary>
public class ContainerHub : Hub
{
    private static ApiClient client = new ApiClient(ServerContext.ApiKey);
    private static ContainerService service = new ContainerService(client);

    /// <summary>
    /// Sets up a new container asynchronously.
    /// </summary>
    /// <param name="request">The container setup request.</param>
    /// <param name="cancellationToken">Cancellation token (optional).</param>
    public async Task SetupContainerAsync(ContainerSetupRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await service.SetupContainerAsync(request, cancellationToken);
            await Clients.Caller.setupContainerSuccess(result);
        }
        catch (Exception ex)
        {
            await Clients.Caller.setupContainerError(ex.Message ?? "An error occurred during container setup.");
        }
    }
}
