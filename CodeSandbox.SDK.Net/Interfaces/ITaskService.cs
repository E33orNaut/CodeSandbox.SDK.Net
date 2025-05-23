using System;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Models;

namespace CodeSandbox.SDK.Net.Services
{
    /// <summary>
    /// Interface for task-related API operations.
    /// </summary>
    public interface ITaskService
    {
        Task<SuccessResponse> GetTaskListAsync(CancellationToken cancellationToken = default);
        Task<SuccessResponse> RunTaskAsync(string taskId, CancellationToken cancellationToken = default);
        Task<SuccessResponse> RunCommandAsync(string taskId, CancellationToken cancellationToken = default);
        Task<SuccessResponse> StopTaskAsync(string taskId, CancellationToken cancellationToken = default);
        Task<SuccessResponse> CreateTaskAsync(string taskId, CancellationToken cancellationToken = default);
        Task<SuccessResponse> UpdateTaskAsync(string taskId, CancellationToken cancellationToken = default);
        // savetoconfig
        Task<SuccessResponse> SaveToConfigAsync(string taskId, CancellationToken cancellationToken = default);
        // generateconfig
        Task<SuccessResponse> GenerateConfigAsync(string taskId, CancellationToken cancellationToken = default);
 


        /// <summary>
        /// Creates setup tasks asynchronously.
        /// </summary>
        /// <param name="request">Request object with task setup details.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A <see cref="SuccessResponse"/> indicating the result.</returns>
        /// <exception cref="TaskServiceException">Thrown when API returns a 400 error with details.</exception>
        /// <exception cref="Exception">Thrown on deserialization errors, HTTP failures, or unexpected errors.</exception>
        Task<SuccessResponse> CreateSetupTasksAsync(CreateSetupTasksRequest request, CancellationToken cancellationToken = default);
    }
}
