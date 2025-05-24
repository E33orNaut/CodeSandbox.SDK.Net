using System;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Models.New.SandboxTaskModels;

namespace CodeSandbox.SDK.Net.Interfaces
{
    /// <summary>
    /// Interface for task-related API operations.
    /// </summary>
    public interface ITaskService
    {
        Task<SandboxTaskSuccessResponse<SandboxTaskListResult>> GetTaskListAsync(CancellationToken cancellationToken = default);
        Task<SandboxTaskSuccessResponse<SandboxTaskResult>> RunTaskAsync(string taskId, CancellationToken cancellationToken = default);
        Task<SandboxTaskSuccessResponse<SandboxTaskResult>> RunCommandAsync(string taskId, string command, CancellationToken cancellationToken = default);
        Task<SandboxTaskSuccessResponse<SandboxTaskResult>> StopTaskAsync(string taskId, CancellationToken cancellationToken = default);
        Task<SandboxTaskSuccessResponse<SandboxTaskResult>> CreateTaskAsync(string taskId, CancellationToken cancellationToken = default);
        Task<SandboxTaskSuccessResponse<SandboxTaskResult>> UpdateTaskAsync(string taskId, CancellationToken cancellationToken = default);
        Task<SandboxTaskSuccessResponse<SandboxTaskResult>> SaveToConfigAsync(string taskId, CancellationToken cancellationToken = default);
        Task<SandboxTaskSuccessResponse<SandboxTaskResult>> GenerateConfigAsync(string taskId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates setup tasks asynchronously.
        /// </summary>
        /// <param name="request">Request object with task setup details.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A <see cref="SandboxTaskSuccessResponse{SandboxTaskSetupTasksResult}"/> indicating the result.</returns>
        Task<SandboxTaskSuccessResponse<SandboxTaskSetupTasksResult>> CreateSetupTasksAsync(SandboxTaskCreateSetupTasksRequest request, CancellationToken cancellationToken = default);
    }
}