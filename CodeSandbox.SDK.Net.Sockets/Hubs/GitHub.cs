using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Interfaces;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models.New.GitModels;
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
            await Clients.Caller.getStatusAsync(response);
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
            await Clients.Caller.getRemotesAsync(response);
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
            await Clients.Caller.getTargetDiffAsync(response);
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
            await Clients.Caller.postDiscardAsync(response);
        }
        else
        {
            await Clients.Caller.sendError("Failed to retrieve target diff.");
        }
    }

    public async Task PostCommitAsync(string message)
    {
        var response = await service.PostCommitAsync(message, CancellationToken.None);
        if (response != null)
        {
            await Clients.Caller.postCommitAsync(response);
        }
        else
        {
            await Clients.Caller.sendError("Failed to commit changes.");
        }
    }

    public async Task PostRemoteAddAsync(string url)
    {
        await service.PostRemoteAddAsync(url, CancellationToken.None); 
    }

    public async Task PostPushAsync()
    {
        await service.PostPushAsync(CancellationToken.None);
        
    }

    public async Task PostPushToRemoteAsync(string url, string branch, bool squashAllCommits = false)
    {
        await service.PostPushToRemoteAsync(url, branch, squashAllCommits, CancellationToken.None);
    }

    public async Task PostRenameBranchAsync(string oldBranch, string newBranch)
    {
        await service.PostRenameBranchAsync(oldBranch, newBranch, CancellationToken.None);
    }

    public async Task PostRemoteContentAsync(string refrence, string path)

    {
        var response = await service.PostRemoteContentAsync(refrence, path, CancellationToken.None);
        if (response != null)
        {
            await Clients.Caller.postRemoteContentAsync(response);
        }
        else
        {
            await Clients.Caller.sendError("Failed to retrieve remote content.");
        }
    }

    public async Task PostDiffStatusAsync(string baseRef, string headRef)
    {
        var response = await service.PostDiffStatusAsync(baseRef, headRef, CancellationToken.None);
        if (response != null)
        {
            await Clients.Caller.postDiffStatusAsync(response);
        }
        else
        {
            await Clients.Caller.sendError("Failed to retrieve diff status.");
        }
    }

    public async Task PostResetLocalWithRemoteAsync()
    {
               await service.PostResetLocalWithRemoteAsync(CancellationToken.None);
    }

    public async Task PostCheckoutInitialBranchAsync()
    {
               await service.PostCheckoutInitialBranchAsync(CancellationToken.None);
    }

    public async Task PostTransposeLinesAsync(List<GitTransposeLinesResultItem> requests)
    {         
        var response = await service.PostTransposeLinesAsync(requests, CancellationToken.None);
        if (response != null)
        {
            await Clients.Caller.postTransposeLinesAsync(response);
        }
        else
        {
            await Clients.Caller.sendError("Failed to transpose lines.");
        }
    }

}
