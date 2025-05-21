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
