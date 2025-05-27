using System;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models.New.SandboxShellModels;
using CodeSandbox.SDK.Net.Services;
using Microsoft.AspNet.SignalR;

namespace CodeSandbox.SDK.Net.Sockets.Hubs
{
    public class ShellServiceHub : Hub
    {
        private static ApiClient client = new ApiClient(ServerContext.ApiKey);
        private static ShellService service = new ShellService(client);

        public async Task<object> CreateShell(SandboxShellCreateRequest request)
        {
            try
            {
                var result = await service.CreateShellAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        public async Task<object> SendInput(SandboxShellInRequest request)
        {
            try
            {
                var result = await service.SendInputAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        public async Task<object> ListShells()
        {
            try
            {
                var result = await service.ListShellsAsync();
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        public async Task<object> OpenShell(SandboxShellOpenRequest request)
        {
            try
            {
                var result = await service.OpenShellAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        public async Task<object> CloseShell(SandboxShellIdRequest request)
        {
            try
            {
                var result = await service.CloseShellAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        public async Task<object> RestartShell(SandboxShellIdRequest request)
        {
            try
            {
                var result = await service.RestartShellAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        public async Task<object> TerminateShell(SandboxShellIdRequest request)
        {
            try
            {
                var result = await service.TerminateShellAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        public async Task<object> ResizeShell(SandboxShellResizeRequest request)
        {
            try
            {
                var result = await service.ResizeShellAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        public async Task<object> RenameShell(SandboxShellRenameRequest request)
        {
            try
            {
                var result = await service.RenameShellAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }
    }
}
