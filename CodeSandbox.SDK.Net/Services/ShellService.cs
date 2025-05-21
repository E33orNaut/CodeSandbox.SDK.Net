using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Interfaces;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models;
using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Services
{
    /// <summary>
    /// Service for shell-related API operations.
    /// </summary>
    public class ShellService : IShellService
    {
        private readonly HttpClient _httpClient;
        private readonly LoggerService _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellService"/> class.
        /// </summary>
        /// <param name="httpClient">HTTP client for requests.</param>
        /// <param name="logger">Logger service.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="httpClient"/> or <paramref name="logger"/> is <c>null</c>.</exception>
        public ShellService(HttpClient httpClient, LoggerService logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Renames a shell asynchronously by sending a rename request to the API.
        /// </summary>
        /// <param name="request">The shell rename request containing necessary information.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
        /// <returns>A <see cref="SuccessResponse"/> indicating the result of the rename operation.</returns>
        /// <exception cref="ApiException">Thrown when the API returns an error response.</exception>
        /// <exception cref="Exception">Throws when HTTP request fails, JSON deserialization fails, or an unexpected error occurs.</exception>
        public async Task<SuccessResponse> RenameShellAsync(ShellRenameRequest request, CancellationToken cancellationToken = default)
        {
            string url = "/shell/rename";

            try
            {
                _logger.LogTrace($"RenameShellAsync called with request: {JsonConvert.SerializeObject(request)}");

                string json = JsonConvert.SerializeObject(request);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content, cancellationToken);

                string responseJson = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        SuccessResponse successResponse = JsonConvert.DeserializeObject<SuccessResponse>(responseJson);
                        _logger.LogSuccess($"RenameShellAsync succeeded: {JsonConvert.SerializeObject(successResponse)}");
                        return successResponse;
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError($"Failed to deserialize success response JSON in RenameShellAsync: {jsonEx.Message}");
                        throw new Exception("Failed to deserialize success response JSON.", jsonEx);
                    }
                }
                else
                {
                    try
                    {
                        ErrorResponse error = JsonConvert.DeserializeObject<ErrorResponse>(responseJson);
                        _logger.LogWarning($"API error in RenameShellAsync: Code={error.Error?.Code}, Message={error.Error?.Message}");
                        throw new ApiException(error.Error?.Code.ToString(), error.Error?.Message);
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError($"API error response deserialization failed in RenameShellAsync. Status code: {response.StatusCode}, Error: {jsonEx.Message}");
                        throw new Exception($"API error response deserialization failed. Status code: {response.StatusCode}", jsonEx);
                    }
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError($"HTTP request failed in RenameShellAsync: {httpEx.Message}");
                throw new Exception("HTTP request failed in RenameShellAsync.", httpEx);
            }
            catch (Exception ex) when (!(ex is ApiException))
            {
                _logger.LogError($"Unexpected error in RenameShellAsync: {ex.Message}");
                throw new Exception("Unexpected error in RenameShellAsync.", ex);
            }
        }
    }

    /// <summary>
    /// Exception thrown when the API returns an error response.
    /// </summary>
    public class ApiException : Exception
    {
        /// <summary>
        /// Gets the error code returned by the API.
        /// </summary>
        public string ErrorCode { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class with a specified error code and message.
        /// </summary>
        /// <param name="code">The error code returned by the API.</param>
        /// <param name="message">The error message returned by the API.</param>
        public ApiException(string code, string message) : base(message)
        {
            ErrorCode = code;
        }
    }
}
