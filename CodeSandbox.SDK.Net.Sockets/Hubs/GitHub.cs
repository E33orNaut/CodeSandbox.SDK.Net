using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Interfaces;
using CodeSandbox.SDK.Net.Internal; 
using CodeSandbox.SDK.Net.Services;
using CodeSandbox.SDK.Net.Sockets;
using Microsoft.AspNet.SignalR;

public class GitHib : Hub
{
    private static ApiClient client = new ApiClient(ServerContext.ApiKey);
    private static GitService service = new GitService(client);

    public async Task GetStatusAsync() 
    {
        var response = await service.GetStatusAsync(CancellationToken.None);
        if (response != null) 
        {
            await Clients.Caller.sendStatus(response);
        } 
        else 
        {
            await Clients.Caller.sendError("Failed to retrieve status.");
        }
         
    } 

    public async Task GetRemotesAsync() 
    {
        var response = await service.GetRemotesAsync(CancellationToken.None);
        if (response != null) 
        {
            await Clients.Caller.sendRemote(response);
        } 
        else 
        {
            await Clients.Caller.sendError("Failed to retrieve remote information.");
        }
    }

    public async Task GetTargetDiffAsync(string branch) 
    {
        var response = await service.GetTargetDiffAsync(branch, CancellationToken.None);
        if (response != null) 
        {
            await Clients.Caller.sendTargetDiff(response);
        } 
        else 
        {
            await Clients.Caller.sendError("Failed to retrieve target diff.");
        }
    }

    public async Task PostPullAsync(string branch) 
    {
        await service.PostPullAsync(branch, force: false, CancellationToken.None);
        
    }

    public async Task PostDiscardAsync(string[] paths)        
    {
        var response =  await service.PostDiscardAsync(paths, CancellationToken.None);
        if (response != null)
        {
            await Clients.Caller.sendTargetDiff(response);
        }
        else
        {
            await Clients.Caller.sendError("Failed to retrieve target diff.");
        }
    }


}
