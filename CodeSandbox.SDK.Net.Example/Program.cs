using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeSandbox.SDK.Models;
using CodeSandbox.SDK.Net;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models;
using CodeSandbox.SDK.Net.Services;
using CodeSandbox.SDK.Services;

namespace CodeSandbox.SDK.Net.Example
{
    internal class Program
    {
        static async Task Main()
        {
            var client = new ApiClient("api-token");

            // ContainerService example
            var containerService = new ContainerService(client);
            var containerRequest = new ContainerSetupRequest { TemplateId = "template-id" };
            var containerResponse = await containerService.SetupContainerAsync(containerRequest);
            Console.WriteLine($"Container setup result: {containerResponse.Result}");

            // GitService examples
            var gitService = new GitService(client);
            await gitService.PostCommitAsync("Initial commit");
            var gitStatus = await gitService.GetStatusAsync();
            Console.WriteLine($"Git status: {gitStatus.Status}"); 
            await gitService.PostPullAsync("main");
            await gitService.PostDiscardAsync(new[] { "file.txt" });
            await gitService.PostRemoteAddAsync("https://github.com/user/repo.git");

            // PortService example
            var portService = new PortService(client);
            var portList = await portService.GetPortListAsync();
            foreach (var port in portList.Result.List)
                Console.WriteLine($"Port: {port.PortNumber}, Url: {port.Url}");

            // SandboxFsService examples
            var fsService = new SandboxFsService(client);
            await fsService.WriteFileAsync(new WriteFileRequest { Path = "file.txt", Content = "Hello World" });
            await fsService.FsReadFileAsync(new FSReadFileParams { Path = "file.txt" });
            await fsService.FsUploadAsync(new UploadRequest { ParentId = "root", Content = "data" });
            await fsService.FsDownloadAsync(new DownloadRequest { Path = "file.txt" });
            await fsService.FsPathSearchAsync(new PathSearchParams());
            await fsService.StatAsync(new FSStatParams { Path = "file.txt" });
            await fsService.CopyAsync(new FSCopyParams { SourcePath = "file.txt" });
            await fsService.RenameAsync(new FSRenameParams { /* fill as needed */ });
            await fsService.RemoveAsync(new FSRemoveParams { Path = "file.txt" });
            await fsService.ReadDirAsync(new FSReadDirParams { Path = "." });

            // SetupService examples
            var setupService = new SetupService(client);
            await setupService.InitializeSetupAsync();
            await setupService.GetSetupProgressAsync();
            await setupService.SkipStepAsync(0);
            await setupService.SkipAllStepsAsync();
            await setupService.EnableSetupAsync();
            await setupService.DisableSetupAsync();
            await setupService.SetStepAsync(0);
        }
    }
}
