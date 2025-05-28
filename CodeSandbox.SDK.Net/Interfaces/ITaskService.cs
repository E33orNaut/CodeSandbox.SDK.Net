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
        /// <summary>
        /// Retrieves the list of tasks and setup tasks.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task representing the asynchronous operation, with a <see cref="SandboxTaskSuccessResponse{SandboxTaskListResult}"/>
        /// containing the list of tasks, setup tasks, and any validation errors.
        /// </returns>
        Task<SandboxTaskSuccessResponse<SandboxTaskListResult>> GetTaskListAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Runs a task by its identifier.
        /// </summary>
        /// <param name="taskId">The unique identifier of the task to run.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task representing the asynchronous operation, with a <see cref="SandboxTaskSuccessResponse{SandboxTaskResult}"/>
        /// containing the result of the run operation.
        /// </returns>
        Task<SandboxTaskSuccessResponse<SandboxTaskResult>> RunTaskAsync(string taskId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Runs a command in the context of a specific task.
        /// </summary>
        /// <param name="taskId">The unique identifier of the task.</param>
        /// <param name="command">The command to execute.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task representing the asynchronous operation, with a <see cref="SandboxTaskSuccessResponse{SandboxTaskResult}"/>
        /// containing the result of the command execution.
        /// </returns>
        Task<SandboxTaskSuccessResponse<SandboxTaskResult>> RunCommandAsync(string taskId, string command, CancellationToken cancellationToken = default);

        /// <summary>
        /// Stops a running task by its identifier.
        /// </summary>
        /// <param name="taskId">The unique identifier of the task to stop.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task representing the asynchronous operation, with a <see cref="SandboxTaskSuccessResponse{SandboxTaskResult}"/>
        /// containing the result of the stop operation.
        /// </returns>
        Task<SandboxTaskSuccessResponse<SandboxTaskResult>> StopTaskAsync(string taskId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a new task by its identifier.
        /// </summary>
        /// <param name="taskId">The unique identifier for the new task.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task representing the asynchronous operation, with a <see cref="SandboxTaskSuccessResponse{SandboxTaskResult}"/>
        /// containing the result of the create operation.
        /// </returns>
        Task<SandboxTaskSuccessResponse<SandboxTaskResult>> CreateTaskAsync(string taskId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing task by its identifier.
        /// </summary>
        /// <param name="taskId">The unique identifier of the task to update.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task representing the asynchronous operation, with a <see cref="SandboxTaskSuccessResponse{SandboxTaskResult}"/>
        /// containing the result of the update operation.
        /// </returns>
        Task<SandboxTaskSuccessResponse<SandboxTaskResult>> UpdateTaskAsync(string taskId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Saves a task to the configuration by its identifier.
        /// </summary>
        /// <param name="taskId">The unique identifier of the task to save.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task representing the asynchronous operation, with a <see cref="SandboxTaskSuccessResponse{SandboxTaskResult}"/>
        /// containing the result of the save operation.
        /// </returns>
        Task<SandboxTaskSuccessResponse<SandboxTaskResult>> SaveToConfigAsync(string taskId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Generates configuration for a task by its identifier.
        /// </summary>
        /// <param name="taskId">The unique identifier of the task to generate configuration for.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task representing the asynchronous operation, with a <see cref="SandboxTaskSuccessResponse{SandboxTaskResult}"/>
        /// containing the result of the generate configuration operation.
        /// </returns>
        Task<SandboxTaskSuccessResponse<SandboxTaskResult>> GenerateConfigAsync(string taskId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates setup tasks asynchronously.
        /// </summary>
        /// <param name="request">Request object with task setup details.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="SandboxTaskSuccessResponse{SandboxTaskSetupTasksResult}"/> indicating the result of the setup tasks creation.
        /// </returns>
        Task<SandboxTaskSuccessResponse<SandboxTaskSetupTasksResult>> CreateSetupTasksAsync(SandboxTaskCreateSetupTasksRequest request, CancellationToken cancellationToken = default);
    }
}