using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            // nuget install CodeSandbox.SDK.Net
            // dotnet add package CodeSandbox.SDK.Net --version 0.0.11

            // Set your codesandbox.io api key here
            var client = new ApiClient("api-token");



            // CREATE CONTAINER EXAMPLE
            // Configure the containerSetupRequest with the template ID and features
            ContainerSetupRequest request = new ContainerSetupRequest
            {
                TemplateId = "something-i-dont-actually-know"
            };
            _ = new List<ContainerSetupFeature>
            {
                new ContainerSetupFeature()
            };

            // Execut the container setup request and get the result
            var files = await new ContainerService(client).SetupContainerAsync(request);

            // Printe the result.
            Console.WriteLine(files.Result);


            // PUSH COMMIT EXAMPLE
            await new GitService(client).PostCommitAsync("Commit message");

            // GIT STATUS DIFF STATUS
            await new GitService(client).GetTargetDiffAsync("branch");

            // GET PORT LIST EXAMPLE
            var ports = await new PortService(client).GetPortListAsync(cancellationToken: default);
            foreach (var x in ports.Result.List)
            {
                Console.WriteLine($"Port: {x.PortNumber}, Url: {x.Url} ");
            }

            // WRITE A FILE IN SANDBOX
            WriteFileRequest writeRequest = new WriteFileRequest
            {
                Content = "Hello World!",
                Path = "path/to/file.txt",
            };
            await new SandboxFsService(client).WriteFileAsync(writeRequest);

            // INITALIZE SETUP EXAMPLE
            await new SetupService(client).InitializeSetupAsync();


        }
    }
}
