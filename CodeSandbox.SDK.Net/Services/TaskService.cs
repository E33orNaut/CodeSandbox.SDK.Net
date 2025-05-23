using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models;

namespace CodeSandbox.SDK.Net.Services
{
    /// <summary>
    /// Provides task-related API operations.
    /// </summary>
    public class TaskService : ITaskService
    {
        private readonly HttpClient _httpClient;
        private readonly LoggerService _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="TaskService"/>.
        /// </summary>
        /// <param name="httpClient">HTTP client for sending requests.</param>
        /// <param name="logger">Logger service for logging.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="httpClient"/> or <paramref name="logger"/> is null.</exception>
        public TaskService(HttpClient httpClient, LoggerService logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<SuccessResponse> GetTaskListAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("GetTaskList called.");
            try
            {
                string url = "/task/list";
                HttpResponseMessage response = await _httpClient.GetAsync(url, cancellationToken);
                string responseString = await response.Content.ReadAsStringAsync();
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        SuccessResponse result = JsonSerializer.Deserialize<SuccessResponse>(responseString, options);
                        _logger.LogSuccess("GetTaskList succeeded.");
                        return result;
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError($"Failed to deserialize success response JSON in GetTaskList: {jsonEx.Message}");
                        throw new Exception("Failed to deserialize success response JSON in GetTaskList.", jsonEx);
                    }
                }
                else if ((int)response.StatusCode == 400)
                {
                    try
                    {
                        ErrorResponse error = JsonSerializer.Deserialize<ErrorResponse>(responseString, options);
                        _logger.LogError($"API returned error in GetTaskList: Code={error.Error.Code}, Message={error.Error.Message}");
                        throw new TaskServiceException(error.Error.Code, error.Error.Message);
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError($"Failed to deserialize error response JSON in GetTaskList: {jsonEx.Message}");
                        throw new Exception("Failed to deserialize error response JSON in GetTaskList.", jsonEx);
                    }
                }
                else
                {
                    _logger.LogError($"Unexpected HTTP status code {response.StatusCode} in GetTaskList.");
                    _ = response.EnsureSuccessStatusCode();
                    return null; // Shut up the wife, i mean compiler
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


        public async Task<SuccessResponse> RunTaskAsync(string taskId, CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("RunTask called.");
            try
            {
                string url = $"/task/run/{taskId}";
                HttpResponseMessage response = await _httpClient.GetAsync(url, cancellationToken);
                string responseString = await response.Content.ReadAsStringAsync();
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        SuccessResponse result = JsonSerializer.Deserialize<SuccessResponse>(responseString, options);
                        _logger.LogSuccess("RunTask succeeded.");
                        return result;
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError($"Failed to deserialize success response JSON in RunTask: {jsonEx.Message}");
                        throw new Exception("Failed to deserialize success response JSON in RunTask.", jsonEx);
                    }
                }
                else if ((int)response.StatusCode == 400)
                {
                    try
                    {
                        ErrorResponse error = JsonSerializer.Deserialize<ErrorResponse>(responseString, options);
                        _logger.LogError($"API returned error in RunTask: Code={error.Error.Code}, Message={error.Error.Message}");
                        throw new TaskServiceException(error.Error.Code, error.Error.Message);
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError($"Failed to deserialize error response JSON in RunTask: {jsonEx.Message}");
                        throw new Exception("Failed to deserialize error response JSON in RunTask.", jsonEx);
                    }
                }
                else
                {
                    _logger.LogError($"Unexpected HTTP status code {response.StatusCode} in RunTask.");
                    _ = response.EnsureSuccessStatusCode();
                    return null; // Shut up the wife, i mean compiler
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



        public async Task<SuccessResponse> RunCommandAsync(string taskId, string command, CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("RunCommand called.");
            try
            {
                string url = $"/task/run/{taskId}";
                var request = new RunCommandRequest { Command = command };
                string json = JsonSerializer.Serialize(request, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(url, content, cancellationToken);
                string responseString = await response.Content.ReadAsStringAsync();
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        SuccessResponse result = JsonSerializer.Deserialize<SuccessResponse>(responseString, options);
                        _logger.LogSuccess("RunCommand succeeded.");
                        return result;
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError($"Failed to deserialize success response JSON in RunCommand: {jsonEx.Message}");
                        throw new Exception("Failed to deserialize success response JSON in RunCommand.", jsonEx);
                    }
                }
                else if ((int)response.StatusCode == 400)
                {
                    try
                    {
                        ErrorResponse error = JsonSerializer.Deserialize<ErrorResponse>(responseString, options);
                        _logger.LogError($"API returned error in RunCommand: Code={error.Error.Code}, Message={error.Error.Message}");
                        throw new TaskServiceException(error.Error.Code, error.Error.Message);
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError($"Failed to deserialize error response JSON in RunCommand: {jsonEx.Message}");
                        throw new Exception("Failed to deserialize error response JSON in RunCommand.", jsonEx);
                    }
                }
                else
                {
                    _logger.LogError($"Unexpected HTTP status code {response.StatusCode} in RunCommand.");
                    _ = response.EnsureSuccessStatusCode();
                    return null; // Shut up the wife, i mean compiler
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


        public async Task<SuccessResponse> StopTaskAsync(string taskId, CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("StopTask called.");
            try
            {
                string url = $"/task/stop/{taskId}";
                HttpResponseMessage response = await _httpClient.GetAsync(url, cancellationToken);
                string responseString = await response.Content.ReadAsStringAsync();
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        SuccessResponse result = JsonSerializer.Deserialize<SuccessResponse>(responseString, options);
                        _logger.LogSuccess("StopTask succeeded.");
                        return result;
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError($"Failed to deserialize success response JSON in StopTask: {jsonEx.Message}");
                        throw new Exception("Failed to deserialize success response JSON in StopTask.", jsonEx);
                    }
                }
                else if ((int)response.StatusCode == 400)
                {
                    try
                    {
                        ErrorResponse error = JsonSerializer.Deserialize<ErrorResponse>(responseString, options);
                        _logger.LogError($"API returned error in StopTask: Code={error.Error.Code}, Message={error.Error.Message}");
                        throw new TaskServiceException(error.Error.Code, error.Error.Message);
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError($"Failed to deserialize error response JSON in StopTask: {jsonEx.Message}");
                        throw new Exception("Failed to deserialize error response JSON in StopTask.", jsonEx);
                    }
                }
                else
                {
                    _logger.LogError($"Unexpected HTTP status code {response.StatusCode} in StopTask.");
                    _ = response.EnsureSuccessStatusCode();
                    return null; // Shut up the wife, i mean compiler
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

        public async Task<SuccessResponse> CreateTaskAsync(string taskId, CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("CreateTask called.");
            try
            {
                string url = $"/task/create/{taskId}";
                HttpResponseMessage response = await _httpClient.GetAsync(url, cancellationToken);
                string responseString = await response.Content.ReadAsStringAsync();
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        SuccessResponse result = JsonSerializer.Deserialize<SuccessResponse>(responseString, options);
                        _logger.LogSuccess("CreateTask succeeded.");
                        return result;
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError($"Failed to deserialize success response JSON in CreateTask: {jsonEx.Message}");
                        throw new Exception("Failed to deserialize success response JSON in CreateTask.", jsonEx);
                    }
                }
                else if ((int)response.StatusCode == 400)
                {
                    try
                    {
                        ErrorResponse error = JsonSerializer.Deserialize<ErrorResponse>(responseString, options);
                        _logger.LogError($"API returned error in CreateTask: Code={error.Error.Code}, Message={error.Error.Message}");
                        throw new TaskServiceException(error.Error.Code, error.Error.Message);
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError($"Failed to deserialize error response JSON in CreateTask: {jsonEx.Message}");
                        throw new Exception("Failed to deserialize error response JSON in CreateTask.", jsonEx);
                    }
                }
                else
                {
                    _logger.LogError($"Unexpected HTTP status code {response.StatusCode} in CreateTask.");
                    _ = response.EnsureSuccessStatusCode();
                    return null; // Shut up the wife, i mean compiler
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

        public async Task<SuccessResponse> UpdateTaskAsync(string taskId, CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("UpdateTask called.");
            try
            {
                string url = $"/task/update/{taskId}";
                HttpResponseMessage response = await _httpClient.GetAsync(url, cancellationToken);
                string responseString = await response.Content.ReadAsStringAsync();
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        SuccessResponse result = JsonSerializer.Deserialize<SuccessResponse>(responseString, options);
                        _logger.LogSuccess("UpdateTask succeeded.");
                        return result;
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError($"Failed to deserialize success response JSON in UpdateTask: {jsonEx.Message}");
                        throw new Exception("Failed to deserialize success response JSON in UpdateTask.", jsonEx);
                    }
                }
                else if ((int)response.StatusCode == 400)
                {
                    try
                    {
                        ErrorResponse error = JsonSerializer.Deserialize<ErrorResponse>(responseString, options);
                        _logger.LogError($"API returned error in UpdateTask: Code={error.Error.Code}, Message={error.Error.Message}");
                        throw new TaskServiceException(error.Error.Code, error.Error.Message);
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError($"Failed to deserialize error response JSON in UpdateTask: {jsonEx.Message}");
                        throw new Exception("Failed to deserialize error response JSON in UpdateTask.", jsonEx);
                    }
                }
                else
                {
                    _logger.LogError($"Unexpected HTTP status code {response.StatusCode} in UpdateTask.");
                    _ = response.EnsureSuccessStatusCode();
                    return null; // Shut up the wife, i mean compiler
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



        public async Task<SuccessResponse> SaveToConfigAsync(string taskId, CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("SaveToConfig called.");
            try
            {
                string url = $"/task/save/{taskId}";
                HttpResponseMessage response = await _httpClient.GetAsync(url, cancellationToken);
                string responseString = await response.Content.ReadAsStringAsync();
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        SuccessResponse result = JsonSerializer.Deserialize<SuccessResponse>(responseString, options);
                        _logger.LogSuccess("SaveToConfig succeeded.");
                        return result;
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError($"Failed to deserialize success response JSON in SaveToConfig: {jsonEx.Message}");
                        throw new Exception("Failed to deserialize success response JSON in SaveToConfig.", jsonEx);
                    }
                }
                else if ((int)response.StatusCode == 400)
                {
                    try
                    {
                        ErrorResponse error = JsonSerializer.Deserialize<ErrorResponse>(responseString, options);
                        _logger.LogError($"API returned error in SaveToConfig: Code={error.Error.Code}, Message={error.Error.Message}");
                        throw new TaskServiceException(error.Error.Code, error.Error.Message);
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError($"Failed to deserialize error response JSON in SaveToConfig: {jsonEx.Message}");
                        throw new Exception("Failed to deserialize error response JSON in SaveToConfig.", jsonEx);
                    }
                }
                else
                {
                    _logger.LogError($"Unexpected HTTP status code {response.StatusCode} in SaveToConfig.");
                    _ = response.EnsureSuccessStatusCode();
                    return null; // Shut up the wife, i mean compiler
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

        public async Task<SuccessResponse> GenerateConfigAsync(string taskId, CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("GenerateConfig called.");
            try
            {
                string url = $"/task/generate/{taskId}";
                HttpResponseMessage response = await _httpClient.GetAsync(url, cancellationToken);
                string responseString = await response.Content.ReadAsStringAsync();
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        SuccessResponse result = JsonSerializer.Deserialize<SuccessResponse>(responseString, options);
                        _logger.LogSuccess("GenerateConfig succeeded.");
                        return result;
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError($"Failed to deserialize success response JSON in GenerateConfig: {jsonEx.Message}");
                        throw new Exception("Failed to deserialize success response JSON in GenerateConfig.", jsonEx);
                    }
                }
                else if ((int)response.StatusCode == 400)
                {
                    try
                    {
                        ErrorResponse error = JsonSerializer.Deserialize<ErrorResponse>(responseString, options);
                        _logger.LogError($"API returned error in GenerateConfig: Code={error.Error.Code}, Message={error.Error.Message}");
                        throw new TaskServiceException(error.Error.Code, error.Error.Message);
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError($"Failed to deserialize error response JSON in GenerateConfig: {jsonEx.Message}");
                        throw new Exception("Failed to deserialize error response JSON in GenerateConfig.", jsonEx);
                    }
                }
                else
                {
                    _logger.LogError($"Unexpected HTTP status code {response.StatusCode} in GenerateConfig.");
                    _ = response.EnsureSuccessStatusCode();
                    return null; // Shut up the wife, i mean compiler
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
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A <see cref="SuccessResponse"/> indicating the result.</returns>
        /// <exception cref="TaskServiceException">Thrown when API returns a 400 error with details.</exception>
        /// <exception cref="Exception">Thrown on deserialization errors, HTTP failures, or unexpected errors.</exception>
        public async Task<SuccessResponse> CreateSetupTasksAsync(CreateSetupTasksRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("CreateSetupTasksAsync called.");

            try
            {
                string url = "/task/createSetupTasks";
                string json = JsonSerializer.Serialize(request, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content, cancellationToken);
                string responseString = await response.Content.ReadAsStringAsync();

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        SuccessResponse result = JsonSerializer.Deserialize<SuccessResponse>(responseString, options);
                        _logger.LogSuccess("CreateSetupTasksAsync succeeded.");
                        return result;
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError($"Failed to deserialize success response JSON in CreateSetupTasksAsync: {jsonEx.Message}");
                        throw new Exception("Failed to deserialize success response JSON in CreateSetupTasksAsync.", jsonEx);
                    }
                }
                else if ((int)response.StatusCode == 400)
                {
                    try
                    {
                        ErrorResponse error = JsonSerializer.Deserialize<ErrorResponse>(responseString, options);
                        _logger.LogError($"API returned error in CreateSetupTasksAsync: Code={error.Error.Code}, Message={error.Error.Message}");
                        throw new TaskServiceException(error.Error.Code, error.Error.Message);
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError($"Failed to deserialize error response JSON in CreateSetupTasksAsync: {jsonEx.Message}");
                        throw new Exception("Failed to deserialize error response JSON in CreateSetupTasksAsync.", jsonEx);
                    }
                }
                else
                {
                    _logger.LogError($"Unexpected HTTP status code {response.StatusCode} in CreateSetupTasksAsync.");
                    _ = response.EnsureSuccessStatusCode();
                    return null; // Shut up the wife, i mean compiler
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
