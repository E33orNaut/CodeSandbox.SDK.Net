using System;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Interfaces;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models.New.SandboxTaskModels;

namespace CodeSandbox.SDK.Net.Services
{
    /// <summary>
    /// Provides operations for managing and executing sandbox tasks via the CodeSandbox API.
    /// </summary>
    public class TaskService : ITaskService
    {
        private readonly IApiClient _httpClient;
        private readonly LoggerService _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="TaskService"/>.
        /// </summary>
        /// <param name="httpClient">HTTP client for sending requests. Cannot be null.</param>
        /// <param name="logger">Logger service for logging. Cannot be null.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="httpClient"/> or <paramref name="logger"/> is null.</exception>
        public TaskService(IApiClient httpClient, LoggerService logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? new LoggerService(LogLevel.Trace);
        }

        /// <summary>
        /// Retrieves the list of tasks and setup tasks.
        /// </summary>
        public async Task<SandboxTaskSuccessResponse<SandboxTaskListResult>> GetTaskListAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("GetTaskList called.");
            try
            {
                string url = "/task/list";
                SandboxTaskSuccessResponse<SandboxTaskListResult> result = await _httpClient.GetAsync<SandboxTaskSuccessResponse<SandboxTaskListResult>>(url, cancellationToken);
                _logger.LogSuccess("GetTaskList succeeded.");
                return result;
            }
            catch (ApiException apiEx)
            {
                _logger.LogError($"API error in GetTaskList: {apiEx.StatusCode}, {apiEx.Message}");
                throw new TaskServiceException(apiEx.StatusCode, apiEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in GetTaskList: {ex.Message}");
                throw new Exception("Unexpected error in GetTaskList.", ex);
            }
        }

        /// <summary>
        /// Runs a task by its identifier.
        /// </summary>
        public async Task<SandboxTaskSuccessResponse<SandboxTaskResult>> RunTaskAsync(string taskId, CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("RunTask called.");
            try
            {
                string url = $"/task/run/{taskId}";
                SandboxTaskSuccessResponse<SandboxTaskResult> result = await _httpClient.GetAsync<SandboxTaskSuccessResponse<SandboxTaskResult>>(url, cancellationToken);
                _logger.LogSuccess("RunTask succeeded.");
                return result;
            }
            catch (ApiException apiEx)
            {
                _logger.LogError($"API error in RunTask: {apiEx.StatusCode}, {apiEx.Message}");
                throw new TaskServiceException(apiEx.StatusCode, apiEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in RunTask: {ex.Message}");
                throw new Exception("Unexpected error in RunTask.", ex);
            }
        }

        /// <summary>
        /// Runs a command in the context of a specific task.
        /// </summary>
        public async Task<SandboxTaskSuccessResponse<SandboxTaskResult>> RunCommandAsync(string taskId, string command, CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("RunCommand called.");
            try
            {
                string url = $"/task/run/{taskId}";
                SandboxTaskRunCommandRequest request = new SandboxTaskRunCommandRequest { Command = command };
                SandboxTaskSuccessResponse<SandboxTaskResult> result = await _httpClient.PostAsync<SandboxTaskSuccessResponse<SandboxTaskResult>>(url, request, cancellationToken);
                _logger.LogSuccess("RunCommand succeeded.");
                return result;
            }
            catch (ApiException apiEx)
            {
                _logger.LogError($"API error in RunCommand: {apiEx.StatusCode}, {apiEx.Message}");
                throw new TaskServiceException(apiEx.StatusCode, apiEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in RunCommand: {ex.Message}");
                throw new Exception("Unexpected error in RunCommand.", ex);
            }
        }

        /// <summary>
        /// Stops a running task by its identifier.
        /// </summary>
        public async Task<SandboxTaskSuccessResponse<SandboxTaskResult>> StopTaskAsync(string taskId, CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("StopTask called.");
            try
            {
                string url = $"/task/stop/{taskId}";
                SandboxTaskSuccessResponse<SandboxTaskResult> result = await _httpClient.GetAsync<SandboxTaskSuccessResponse<SandboxTaskResult>>(url, cancellationToken);
                _logger.LogSuccess("StopTask succeeded.");
                return result;
            }
            catch (ApiException apiEx)
            {
                _logger.LogError($"API error in StopTask: {apiEx.StatusCode}, {apiEx.Message}");
                throw new TaskServiceException(apiEx.StatusCode, apiEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in StopTask: {ex.Message}");
                throw new Exception("Unexpected error in StopTask.", ex);
            }
        }

        /// <summary>
        /// Creates a new task by its identifier.
        /// </summary>
        public async Task<SandboxTaskSuccessResponse<SandboxTaskResult>> CreateTaskAsync(string taskId, CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("CreateTask called.");
            try
            {
                string url = $"/task/create/{taskId}";
                SandboxTaskSuccessResponse<SandboxTaskResult> result = await _httpClient.GetAsync<SandboxTaskSuccessResponse<SandboxTaskResult>>(url, cancellationToken);
                _logger.LogSuccess("CreateTask succeeded.");
                return result;
            }
            catch (ApiException apiEx)
            {
                _logger.LogError($"API error in CreateTask: {apiEx.StatusCode}, {apiEx.Message}");
                throw new TaskServiceException(apiEx.StatusCode, apiEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in CreateTask: {ex.Message}");
                throw new Exception("Unexpected error in CreateTask.", ex);
            }
        }

        /// <summary>
        /// Updates an existing task by its identifier.
        /// </summary>
        public async Task<SandboxTaskSuccessResponse<SandboxTaskResult>> UpdateTaskAsync(string taskId, CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("UpdateTask called.");
            try
            {
                string url = $"/task/update/{taskId}";
                SandboxTaskSuccessResponse<SandboxTaskResult> result = await _httpClient.GetAsync<SandboxTaskSuccessResponse<SandboxTaskResult>>(url, cancellationToken);
                _logger.LogSuccess("UpdateTask succeeded.");
                return result;
            }
            catch (ApiException apiEx)
            {
                _logger.LogError($"API error in UpdateTask: {apiEx.StatusCode}, {apiEx.Message}");
                throw new TaskServiceException(apiEx.StatusCode, apiEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in UpdateTask: {ex.Message}");
                throw new Exception("Unexpected error in UpdateTask.", ex);
            }
        }

        /// <summary>
        /// Saves a task to the configuration by its identifier.
        /// </summary>
        public async Task<SandboxTaskSuccessResponse<SandboxTaskResult>> SaveToConfigAsync(string taskId, CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("SaveToConfig called.");
            try
            {
                string url = $"/task/save/{taskId}";
                SandboxTaskSuccessResponse<SandboxTaskResult> result = await _httpClient.GetAsync<SandboxTaskSuccessResponse<SandboxTaskResult>>(url, cancellationToken);
                _logger.LogSuccess("SaveToConfig succeeded.");
                return result;
            }
            catch (ApiException apiEx)
            {
                _logger.LogError($"API error in SaveToConfig: {apiEx.StatusCode}, {apiEx.Message}");
                throw new TaskServiceException(apiEx.StatusCode, apiEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in SaveToConfig: {ex.Message}");
                throw new Exception("Unexpected error in SaveToConfig.", ex);
            }
        }

        /// <summary>
        /// Generates configuration for a task by its identifier.
        /// </summary>
        public async Task<SandboxTaskSuccessResponse<SandboxTaskResult>> GenerateConfigAsync(string taskId, CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("GenerateConfig called.");
            try
            {
                string url = $"/task/generate/{taskId}";
                SandboxTaskSuccessResponse<SandboxTaskResult> result = await _httpClient.GetAsync<SandboxTaskSuccessResponse<SandboxTaskResult>>(url, cancellationToken);
                _logger.LogSuccess("GenerateConfig succeeded.");
                return result;
            }
            catch (ApiException apiEx)
            {
                _logger.LogError($"API error in GenerateConfig: {apiEx.StatusCode}, {apiEx.Message}");
                throw new TaskServiceException(apiEx.StatusCode, apiEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in GenerateConfig: {ex.Message}");
                throw new Exception("Unexpected error in GenerateConfig.", ex);
            }
        }

        /// <summary>
        /// Creates setup tasks asynchronously.
        /// </summary>
        public async Task<SandboxTaskSuccessResponse<SandboxTaskSetupTasksResult>> CreateSetupTasksAsync(SandboxTaskCreateSetupTasksRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("CreateSetupTasksAsync called.");
            try
            {
                string url = "/task/createSetupTasks";
                SandboxTaskSuccessResponse<SandboxTaskSetupTasksResult> result = await _httpClient.PostAsync<SandboxTaskSuccessResponse<SandboxTaskSetupTasksResult>>(url, request, cancellationToken);
                _logger.LogSuccess("CreateSetupTasksAsync succeeded.");
                return result;
            }
            catch (ApiException apiEx)
            {
                _logger.LogError($"API error in CreateSetupTasksAsync: {apiEx.StatusCode}, {apiEx.Message}");
                throw new TaskServiceException(apiEx.StatusCode, apiEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error in CreateSetupTasksAsync: {ex.Message}");
                throw new Exception("Unexpected error in CreateSetupTasksAsync.", ex);
            }
        }
    }

    /// <summary>
    /// Exception thrown for specific TaskService API errors.
    /// </summary>
    public class TaskServiceException : Exception
    {
        /// <summary>
        /// Gets the error code returned by the API.
        /// </summary>
        public int ErrorCode { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="TaskServiceException"/>.
        /// </summary>
        /// <param name="code">API error code.</param>
        /// <param name="message">API error message.</param>
        public TaskServiceException(int code, string message) : base(message)
        {
            ErrorCode = code;
        }
    }
}