using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models.New.SandboxFSModels;
using CodeSandbox.SDK.Net.Services;
using Microsoft.AspNet.SignalR;

namespace CodeSandbox.SDK.Net.Sockets.Hubs
{
    public class SandboxFsHub : Hub
    {
        private static ApiClient client = new ApiClient(ServerContext.ApiKey);
        private static SandboxFsService service = new SandboxFsService(client);

        public async Task WriteFileAsync(SandboxFSWriteFileRequest request)
        {
            try
            {
                var result = await service.WriteFileAsync(request);
                await Clients.Caller.writeFileSuccess(result);
            }
            catch (Exception ex)
            {
                await Clients.Caller.writeFileError(ex.Message ?? "An error occurred while writing the file.");
            }
        }

        public async Task FsPathSearchAsync(SandboxFSPathSearchResult request)
        {
            try
            {
                var result = await service.FsPathSearchAsync(request);
                await Clients.Caller.fsPathSearchSuccess(result);
            }
            catch (Exception ex)
            {
                await Clients.Caller.fsPathSearchError(ex.Message ?? "An error occurred while searching the file system path.");
            }
        }

        public async Task FsUploadAsync(UploadRequest request)
        {
            try
            {
                var result = await service.FsUploadAsync(request);
                await Clients.Caller.fsUploadSuccess(result);
            }
            catch (Exception ex)
            {
                await Clients.Caller.fsUploadError(ex.Message ?? "An error occurred while uploading the file.");
            }
        }

        public async Task FsDownloadAsync(DownloadRequest request)
        {
            try
            {
                var result = await service.FsDownloadAsync(request);
                await Clients.Caller.fsDownloadSuccess(result);
            }
            catch (Exception ex)
            {
                await Clients.Caller.fsDownloadError(ex.Message ?? "An error occurred while downloading the file.");
            }
        }

        public async Task FsReadFileAsync(FSReadFileParams request)
        {
            try
            {
                var result = await service.FsReadFileAsync(request);
                await Clients.Caller.fsReadFileSuccess(result);
            }
            catch (Exception ex)
            {
                await Clients.Caller.fsReadFileError(ex.Message ?? "An error occurred while reading the file.");
            }
        }

        public async Task ReadDirAsync(FSReadDirParams request)
        {
            try
            {
                var result = await service.ReadDirAsync(request);
                await Clients.Caller.readDirSuccess(result);
                
            }
            catch (Exception ex)
            {
                await Clients.Caller.readDirError(ex.Message ?? "An error occurred while reading the directory.");
            }
        }

        public async Task StatAsync(FSStatParams request)
        {
            try
            {
                var result = await service.StatAsync(request);
                await Clients.Caller.statSuccess(result);
            }
            catch (Exception ex)
            {
                await Clients.Caller.statError(ex.Message ?? "An error occurred while retrieving file statistics.");
            }
        }

        public async Task CopyAsync(FSCopyParams request)
        {
            try
            {
                var result = await service.CopyAsync(request);
                await Clients.Caller.copySuccess(result);
            }
            catch (Exception ex)
            {
                await Clients.Caller.copyError(ex.Message ?? "An error occurred while copying the file.");
            }
        }

        public async Task RenameAsync(FSRenameParams request)
        {
            try
            {
                var result = await service.RenameAsync(request);
                await Clients.Caller.renameSuccess(result);
            }
            catch (Exception ex)
            {
                await Clients.Caller.renameError(ex.Message ?? "An error occurred while renaming the file.");
            }
        }

        public async Task RemoveAsync(FSRemoveParams request)
        {
            try
            {
                var result = await service.RemoveAsync(request);
                await Clients.Caller.removeSuccess(result);
            }
            catch (Exception ex)
            {
                await Clients.Caller.removeError(ex.Message ?? "An error occurred while removing the file.");
            }
        }
    }
}
