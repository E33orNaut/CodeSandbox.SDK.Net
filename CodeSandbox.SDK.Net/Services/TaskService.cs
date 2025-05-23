﻿using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        private readonly HttpClient _httpClient;
        private readonly LoggerService _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="TaskService"/>.
        /// </summary>
        /// <param name="httpClient">HTTP client for sending requests. Cannot be null.</param>
        /// <param name="logger">Logger service for logging. Cannot be null.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="httpClient"/> or <paramref name="logger"/> is null.</exception>
        public TaskService(HttpClient httpClient, LoggerService logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves the list of tasks and setup tasks.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task representing the asynchronous operation, with a <see cref="SandboxTaskSuccessResponse{SandboxTaskListResult}"/>
        /// containing the list of tasks, setup tasks, and any validation errors.
        /// </returns>
        /// <exception cref="TaskServiceException">Thrown when the API returns an error response.</exception>
        /// <exception cref="Exception">Thrown on HTTP failure, deserialization failure, or unexpected errors.</exception>
        public async Task<SandboxTaskSuccessResponse<SandboxTaskListResult>> GetTaskListAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("GetTaskList called.");
            try
            {
                string url = "/task/list";
                HttpResponseMessage response = await _httpClient.GetAsync(url, cancellationToken);
                string responseString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<SandboxTaskSuccessResponse<SandboxTaskListResult>>(responseString, options);
                    _logger.LogSuccess("GetTaskList succeeded.");
                    return result;
                }
                else
                {
                    var error = JsonSerializer.Deserialize<SandboxTaskErrorResponse>(responseString, options);
                    _logger.LogError($"API error in GetTaskList: Code={error.Error?.Code}, Message={error.Error?.Message}");
                    throw new TaskServiceException(error.Error?.Code ?? 0, error.Error?.Message ?? "Unknown error");
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError($"HTTP request failed in GetTaskList: {httpEx.Message}");
                throw new Exception("HTTP request failed in GetTaskList.", httpEx);
            }
            catch (Exception ex) when (!(ex is TaskServiceException))
            {
                _logger.LogError($"Unexpected error in GetTaskList: {ex.Message}");
                throw new Exception("Unexpected error in GetTaskList.", ex);
            }
        }

        /// <summary>
        /// Runs a task by its identifier.
        /// </summary>
        /// <param name="taskId">The unique identifier of the task to run.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task representing the asynchronous operation, with a <see cref="SandboxTaskSuccessResponse{SandboxTaskResult}"/>
        /// containing the result of the run operation.
        /// </returns>
        /// <exception cref="TaskServiceException">Thrown when the API returns an error response.</exception>
        /// <exception cref="Exception">Thrown on HTTP failure, deserialization failure, or unexpected errors.</exception>
        public async Task<SandboxTaskSuccessResponse<SandboxTaskResult>> RunTaskAsync(string taskId, CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("RunTask called.");
            try
            {
                string url = $"/task/run/{taskId}";
                HttpResponseMessage response = await _httpClient.GetAsync(url, cancellationToken);
                string responseString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<SandboxTaskSuccessResponse<SandboxTaskResult>>(responseString, options);
                    _logger.LogSuccess("RunTask succeeded.");
                    return result;
                }
                else
                {
                    var error = JsonSerializer.Deserialize<SandboxTaskErrorResponse>(responseString, options);
                    _logger.LogError($"API error in RunTask: Code={error.Error?.Code}, Message={error.Error?.Message}");
                    throw new TaskServiceException(error.Error?.Code ?? 0, error.Error?.Message ?? "Unknown error");
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError($"HTTP request failed in RunTask: {httpEx.Message}");
                throw new Exception("HTTP request failed in RunTask.", httpEx);
            }
            catch (Exception ex) when (!(ex is TaskServiceException))
            {
                _logger.LogError($"Unexpected error in RunTask: {ex.Message}");
                throw new Exception("Unexpected error in RunTask.", ex);
            }
        }

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
        /// <exception cref="TaskServiceException">Thrown when the API returns an error response.</exception>
        /// <exception cref="Exception">Thrown on HTTP failure, deserialization failure, or unexpected errors.</exception>
        public async Task<SandboxTaskSuccessResponse<SandboxTaskResult>> RunCommandAsync(string taskId, string command, CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("RunCommand called.");
            try
            {
                string url = $"/task/run/{taskId}";
                var request = new SandboxTaskRunCommandRequest { Command = command };
                string json = JsonSerializer.Serialize(request, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(url, content, cancellationToken);
                string responseString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<SandboxTaskSuccessResponse<SandboxTaskResult>>(responseString, options);
                    _logger.LogSuccess("RunCommand succeeded.");
                    return result;
                }
                else
                {
                    var error = JsonSerializer.Deserialize<SandboxTaskErrorResponse>(responseString, options);
                    _logger.LogError($"API error in RunCommand: Code={error.Error?.Code}, Message={error.Error?.Message}");
                    throw new TaskServiceException(error.Error?.Code ?? 0, error.Error?.Message ?? "Unknown error");
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError($"HTTP request failed in RunCommand: {httpEx.Message}");
                throw new Exception("HTTP request failed in RunCommand.", httpEx);
            }
            catch (Exception ex) when (!(ex is TaskServiceException))
            {
                _logger.LogError($"Unexpected error in RunCommand: {ex.Message}");
                throw new Exception("Unexpected error in RunCommand.", ex);
            }
        }

        /// <summary>
        /// Stops a running task by its identifier.
        /// </summary>
        /// <param name="taskId">The unique identifier of the task to stop.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task representing the asynchronous operation, with a <see cref="SandboxTaskSuccessResponse{SandboxTaskResult}"/>
        /// containing the result of the stop operation.
        /// </returns>
        /// <exception cref="TaskServiceException">Thrown when the API returns an error response.</exception>
        /// <exception cref="Exception">Thrown on HTTP failure, deserialization failure, or unexpected errors.</exception>
        public async Task<SandboxTaskSuccessResponse<SandboxTaskResult>> StopTaskAsync(string taskId, CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("StopTask called.");
            try
            {
                string url = $"/task/stop/{taskId}";
                HttpResponseMessage response = await _httpClient.GetAsync(url, cancellationToken);
                string responseString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<SandboxTaskSuccessResponse<SandboxTaskResult>>(responseString, options);
                    _logger.LogSuccess("StopTask succeeded.");
                    return result;
                }
                else
                {
                    var error = JsonSerializer.Deserialize<SandboxTaskErrorResponse>(responseString, options);
                    _logger.LogError($"API error in StopTask: Code={error.Error?.Code}, Message={error.Error?.Message}");
                    throw new TaskServiceException(error.Error?.Code ?? 0, error.Error?.Message ?? "Unknown error");
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError($"HTTP request failed in StopTask: {httpEx.Message}");
                throw new Exception("HTTP request failed in StopTask.", httpEx);
            }
            catch (Exception ex) when (!(ex is TaskServiceException))
            {
                _logger.LogError($"Unexpected error in StopTask: {ex.Message}");
                throw new Exception("Unexpected error in StopTask.", ex);
            }
        }

        /// <summary>
        /// Creates a new task by its identifier.
        /// </summary>
        /// <param name="taskId">The unique identifier for the new task.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task representing the asynchronous operation, with a <see cref="SandboxTaskSuccessResponse{SandboxTaskResult}"/>
        /// containing the result of the create operation.
        /// </returns>
        /// <exception cref="TaskServiceException">Thrown when the API returns an error response.</exception>
        /// <exception cref="Exception">Thrown on HTTP failure, deserialization failure, or unexpected errors.</exception>
        public async Task<SandboxTaskSuccessResponse<SandboxTaskResult>> CreateTaskAsync(string taskId, CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("CreateTask called.");
            try
            {
                string url = $"/task/create/{taskId}";
                HttpResponseMessage response = await _httpClient.GetAsync(url, cancellationToken);
                string responseString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<SandboxTaskSuccessResponse<SandboxTaskResult>>(responseString, options);
                    _logger.LogSuccess("CreateTask succeeded.");
                    return result;
                }
                else
                {
                    var error = JsonSerializer.Deserialize<SandboxTaskErrorResponse>(responseString, options);
                    _logger.LogError($"API error in CreateTask: Code={error.Error?.Code}, Message={error.Error?.Message}");
                    throw new TaskServiceException(error.Error?.Code ?? 0, error.Error?.Message ?? "Unknown error");
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError($"HTTP request failed in CreateTask: {httpEx.Message}");
                throw new Exception("HTTP request failed in CreateTask.", httpEx);
            }
            catch (Exception ex) when (!(ex is TaskServiceException))
            {
                _logger.LogError($"Unexpected error in CreateTask: {ex.Message}");
                throw new Exception("Unexpected error in CreateTask.", ex);
            }
        }

        /// <summary>
        /// Updates an existing task by its identifier.
        /// </summary>
        /// <param name="taskId">The unique identifier of the task to update.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task representing the asynchronous operation, with a <see cref="SandboxTaskSuccessResponse{SandboxTaskResult}"/>
        /// containing the result of the update operation.
        /// </returns>
        /// <exception cref="TaskServiceException">Thrown when the API returns an error response.</exception>
        /// <exception cref="Exception">Thrown on HTTP failure, deserialization failure, or unexpected errors.</exception>
        public async Task<SandboxTaskSuccessResponse<SandboxTaskResult>> UpdateTaskAsync(string taskId, CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("UpdateTask called.");
            try
            {
                string url = $"/task/update/{taskId}";
                HttpResponseMessage response = await _httpClient.GetAsync(url, cancellationToken);
                string responseString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<SandboxTaskSuccessResponse<SandboxTaskResult>>(responseString, options);
                    _logger.LogSuccess("UpdateTask succeeded.");
                    return result;
                }
                else
                {
                    var error = JsonSerializer.Deserialize<SandboxTaskErrorResponse>(responseString, options);
                    _logger.LogError($"API error in UpdateTask: Code={error.Error?.Code}, Message={error.Error?.Message}");
                    throw new TaskServiceException(error.Error?.Code ?? 0, error.Error?.Message ?? "Unknown error");
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError($"HTTP request failed in UpdateTask: {httpEx.Message}");
                throw new Exception("HTTP request failed in UpdateTask.", httpEx);
            }
            catch (Exception ex) when (!(ex is TaskServiceException))
            {
                _logger.LogError($"Unexpected error in UpdateTask: {ex.Message}");
                throw new Exception("Unexpected error in UpdateTask.", ex);
            }
        }

        /// <summary>
        /// Saves a task to the configuration by its identifier.
        /// </summary>
        /// <param name="taskId">The unique identifier of the task to save.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task representing the asynchronous operation, with a <see cref="SandboxTaskSuccessResponse{SandboxTaskResult}"/>
        /// containing the result of the save operation.
        /// </returns>
        /// <exception cref="TaskServiceException">Thrown when the API returns an error response.</exception>
        /// <exception cref="Exception">Thrown on HTTP failure, deserialization failure, or unexpected errors.</exception>
        public async Task<SandboxTaskSuccessResponse<SandboxTaskResult>> SaveToConfigAsync(string taskId, CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("SaveToConfig called.");
            try
            {
                string url = $"/task/save/{taskId}";
                HttpResponseMessage response = await _httpClient.GetAsync(url, cancellationToken);
                string responseString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<SandboxTaskSuccessResponse<SandboxTaskResult>>(responseString, options);
                    _logger.LogSuccess("SaveToConfig succeeded.");
                    return result;
                }
                else
                {
                    var error = JsonSerializer.Deserialize<SandboxTaskErrorResponse>(responseString, options);
                    _logger.LogError($"API error in SaveToConfig: Code={error.Error?.Code}, Message={error.Error?.Message}");
                    throw new TaskServiceException(error.Error?.Code ?? 0, error.Error?.Message ?? "Unknown error");
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError($"HTTP request failed in SaveToConfig: {httpEx.Message}");
                throw new Exception("HTTP request failed in SaveToConfig.", httpEx);
            }
            catch (Exception ex) when (!(ex is TaskServiceException))
            {
                _logger.LogError($"Unexpected error in SaveToConfig: {ex.Message}");
                throw new Exception("Unexpected error in SaveToConfig.", ex);
            }
        }

        /// <summary>
        /// Generates configuration for a task by its identifier.
        /// </summary>
        /// <param name="taskId">The unique identifier of the task to generate configuration for.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task representing the asynchronous operation, with a <see cref="SandboxTaskSuccessResponse{SandboxTaskResult}"/>
        /// containing the result of the generate configuration operation.
        /// </returns>
        /// <exception cref="TaskServiceException">Thrown when the API returns an error response.</exception>
        /// <exception cref="Exception">Thrown on HTTP failure, deserialization failure, or unexpected errors.</exception>
        public async Task<SandboxTaskSuccessResponse<SandboxTaskResult>> GenerateConfigAsync(string taskId, CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("GenerateConfig called.");
            try
            {
                string url = $"/task/generate/{taskId}";
                HttpResponseMessage response = await _httpClient.GetAsync(url, cancellationToken);
                string responseString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<SandboxTaskSuccessResponse<SandboxTaskResult>>(responseString, options);
                    _logger.LogSuccess("GenerateConfig succeeded.");
                    return result;
                }
                else
                {
                    var error = JsonSerializer.Deserialize<SandboxTaskErrorResponse>(responseString, options);
                    _logger.LogError($"API error in GenerateConfig: Code={error.Error?.Code}, Message={error.Error?.Message}");
                    throw new TaskServiceException(error.Error?.Code ?? 0, error.Error?.Message ?? "Unknown error");
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError($"HTTP request failed in GenerateConfig: {httpEx.Message}");
                throw new Exception("HTTP request failed in GenerateConfig.", httpEx);
            }
            catch (Exception ex) when (!(ex is TaskServiceException))
            {
                _logger.LogError($"Unexpected error in GenerateConfig: {ex.Message}");
                throw new Exception("Unexpected error in GenerateConfig.", ex);
            }
        }

        /// <summary>
        /// Creates setup tasks asynchronously.
        /// </summary>
        /// <param name="request">Request object with task setup details.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="SandboxTaskSuccessResponse{SandboxTaskSetupTasksResult}"/> indicating the result of the setup tasks creation.
        /// </returns>
        /// <exception cref="TaskServiceException">Thrown when API returns a 400 error with details.</exception>
        /// <exception cref="Exception">Thrown on deserialization errors, HTTP failures, or unexpected errors.</exception>
        public async Task<SandboxTaskSuccessResponse<SandboxTaskSetupTasksResult>> CreateSetupTasksAsync(SandboxTaskCreateSetupTasksRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("CreateSetupTasksAsync called.");
            try
            {
                string url = "/task/createSetupTasks";
                string json = JsonSerializer.Serialize(request, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content, cancellationToken);
                string responseString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<SandboxTaskSuccessResponse<SandboxTaskSetupTasksResult>>(responseString, options);
                    _logger.LogSuccess("CreateSetupTasksAsync succeeded.");
                    return result;
                }
                else
                {
                    var error = JsonSerializer.Deserialize<SandboxTaskErrorResponse>(responseString, options);
                    _logger.LogError($"API error in CreateSetupTasksAsync: Code={error.Error?.Code}, Message={error.Error?.Message}");
                    throw new TaskServiceException(error.Error?.Code ?? 0, error.Error?.Message ?? "Unknown error");
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError($"HTTP request failed in CreateSetupTasksAsync: {httpEx.Message}");
                throw new Exception("HTTP request failed in CreateSetupTasksAsync.", httpEx);
            }
            catch (Exception ex) when (!(ex is TaskServiceException))
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