using System;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Models.New.SandboxShellModels;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Interfaces;

namespace CodeSandbox.SDK.Net.Services
{
    /// <summary>
    /// Provides operations for managing terminal and command shells in the sandbox via the CodeSandbox API.
    /// </summary>
    public class ShellService : IShellService
    {
        private readonly IApiClient _client;
        private readonly LoggerService _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellService"/> class.
        /// </summary>
        /// <param name="client">The API client instance used to communicate with the CodeSandbox API. Cannot be null.</param>
        /// <param name="logger">Optional logger instance for diagnostic output. If not provided, a default logger is used.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="client"/> is null.</exception>
        public ShellService(IApiClient client, LoggerService logger = null)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? new LoggerService(LogLevel.Trace);
        }

        /// <summary>
        /// Creates a new shell (terminal or command) in the sandbox.
        /// </summary>
        /// <param name="request">The <see cref="SandboxShellCreateRequest"/> containing shell creation parameters.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="SandboxShellSuccessResponse{SandboxOpenShellDTO}"/> containing the status and the created shell details.
        /// </returns>
        /// <exception cref="Exception">Thrown if the API call fails or an unexpected error occurs.</exception>
        public async Task<SandboxShellSuccessResponse<SandboxOpenShellDTO>> CreateShellAsync(SandboxShellCreateRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting CreateShellAsync...");
            try
            {
                var response = await _client.PostAsync<SandboxShellSuccessResponse<SandboxOpenShellDTO>>("/shell/create", request, cancellationToken);
                _logger.LogSuccess("CreateShellAsync completed successfully.");
                return response;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in CreateShellAsync: {ex.Message} (Status: {ex.StatusCode})");
                throw new Exception($"API error while creating shell: {ex.Message} (Status: {ex.StatusCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in CreateShellAsync.", ex);
                throw new Exception($"Unexpected error while creating shell: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Sends input to an active shell.
        /// </summary>
        /// <param name="request">The <see cref="SandboxShellInRequest"/> containing the shell ID and input.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="SandboxShellSuccessResponse{object}"/> indicating the result of the input operation.
        /// </returns>
        /// <exception cref="Exception">Thrown if the API call fails or an unexpected error occurs.</exception>
        public async Task<SandboxShellSuccessResponse<object>> SendInputAsync(SandboxShellInRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting SendInputAsync...");
            try
            {
                var response = await _client.PostAsync<SandboxShellSuccessResponse<object>>("/shell/in", request, cancellationToken);
                _logger.LogSuccess("SendInputAsync completed successfully.");
                return response;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in SendInputAsync: {ex.Message} (Status: {ex.StatusCode})");
                throw new Exception($"API error while sending input to shell: {ex.Message} (Status: {ex.StatusCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in SendInputAsync.", ex);
                throw new Exception($"Unexpected error while sending input to shell: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Retrieves the list of all available shells in the sandbox.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="SandboxShellSuccessResponse{SandboxShellListResult}"/> containing the list of shells.
        /// </returns>
        /// <exception cref="Exception">Thrown if the API call fails or an unexpected error occurs.</exception>
        public async Task<SandboxShellSuccessResponse<SandboxShellListResult>> ListShellsAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting ListShellsAsync...");
            try
            {
                var response = await _client.PostAsync<SandboxShellSuccessResponse<SandboxShellListResult>>("/shell/list", new { }, cancellationToken);
                _logger.LogSuccess("ListShellsAsync completed successfully.");
                return response;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in ListShellsAsync: {ex.Message} (Status: {ex.StatusCode})");
                throw new Exception($"API error while listing shells: {ex.Message} (Status: {ex.StatusCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in ListShellsAsync.", ex);
                throw new Exception($"Unexpected error while listing shells: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Opens an existing shell and retrieves its buffer.
        /// </summary>
        /// <param name="request">The <see cref="SandboxShellOpenRequest"/> containing the shell ID and size.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="SandboxShellSuccessResponse{SandboxOpenShellDTO}"/> containing the shell's buffer and details.
        /// </returns>
        /// <exception cref="Exception">Thrown if the API call fails or an unexpected error occurs.</exception>
        public async Task<SandboxShellSuccessResponse<SandboxOpenShellDTO>> OpenShellAsync(SandboxShellOpenRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting OpenShellAsync...");
            try
            {
                var response = await _client.PostAsync<SandboxShellSuccessResponse<SandboxOpenShellDTO>>("/shell/open", request, cancellationToken);
                _logger.LogSuccess("OpenShellAsync completed successfully.");
                return response;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in OpenShellAsync: {ex.Message} (Status: {ex.StatusCode})");
                throw new Exception($"API error while opening shell: {ex.Message} (Status: {ex.StatusCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in OpenShellAsync.", ex);
                throw new Exception($"Unexpected error while opening shell: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Closes a shell without terminating the underlying process.
        /// </summary>
        /// <param name="request">The <see cref="SandboxShellIdRequest"/> containing the shell ID.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="SandboxShellSuccessResponse{object}"/> indicating the result of the close operation.
        /// </returns>
        /// <exception cref="Exception">Thrown if the API call fails or an unexpected error occurs.</exception>
        public async Task<SandboxShellSuccessResponse<object>> CloseShellAsync(SandboxShellIdRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting CloseShellAsync...");
            try
            {
                var response = await _client.PostAsync<SandboxShellSuccessResponse<object>>("/shell/close", request, cancellationToken);
                _logger.LogSuccess("CloseShellAsync completed successfully.");
                return response;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in CloseShellAsync: {ex.Message} (Status: {ex.StatusCode})");
                throw new Exception($"API error while closing shell: {ex.Message} (Status: {ex.StatusCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in CloseShellAsync.", ex);
                throw new Exception($"Unexpected error while closing shell: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Restarts an existing shell process.
        /// </summary>
        /// <param name="request">The <see cref="SandboxShellIdRequest"/> containing the shell ID.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="SandboxShellSuccessResponse{object}"/> indicating the result of the restart operation.
        /// </returns>
        /// <exception cref="Exception">Thrown if the API call fails or an unexpected error occurs.</exception>
        public async Task<SandboxShellSuccessResponse<object>> RestartShellAsync(SandboxShellIdRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting RestartShellAsync...");
            try
            {
                var response = await _client.PostAsync<SandboxShellSuccessResponse<object>>("/shell/restart", request, cancellationToken);
                _logger.LogSuccess("RestartShellAsync completed successfully.");
                return response;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in RestartShellAsync: {ex.Message} (Status: {ex.StatusCode})");
                throw new Exception($"API error while restarting shell: {ex.Message} (Status: {ex.StatusCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in RestartShellAsync.", ex);
                throw new Exception($"Unexpected error while restarting shell: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Terminates a shell and its underlying process.
        /// </summary>
        /// <param name="request">The <see cref="SandboxShellIdRequest"/> containing the shell ID.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="SandboxShellSuccessResponse{SandboxShellDTO}"/> containing the terminated shell's details.
        /// </returns>
        /// <exception cref="Exception">Thrown if the API call fails or an unexpected error occurs.</exception>
        public async Task<SandboxShellSuccessResponse<SandboxShellDTO>> TerminateShellAsync(SandboxShellIdRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting TerminateShellAsync...");
            try
            {
                var response = await _client.PostAsync<SandboxShellSuccessResponse<SandboxShellDTO>>("/shell/terminate", request, cancellationToken);
                _logger.LogSuccess("TerminateShellAsync completed successfully.");
                return response;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in TerminateShellAsync: {ex.Message} (Status: {ex.StatusCode})");
                throw new Exception($"API error while terminating shell: {ex.Message} (Status: {ex.StatusCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in TerminateShellAsync.", ex);
                throw new Exception($"Unexpected error while terminating shell: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Resizes a shell.
        /// </summary>
        /// <param name="request">The <see cref="SandboxShellResizeRequest"/> containing the shell ID and new size.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="SandboxShellSuccessResponse{object}"/> indicating the result of the resize operation.
        /// </returns>
        /// <exception cref="Exception">Thrown if the API call fails or an unexpected error occurs.</exception>
        public async Task<SandboxShellSuccessResponse<object>> ResizeShellAsync(SandboxShellResizeRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting ResizeShellAsync...");
            try
            {
                var response = await _client.PostAsync<SandboxShellSuccessResponse<object>>("/shell/resize", request, cancellationToken);
                _logger.LogSuccess("ResizeShellAsync completed successfully.");
                return response;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in ResizeShellAsync: {ex.Message} (Status: {ex.StatusCode})");
                throw new Exception($"API error while resizing shell: {ex.Message} (Status: {ex.StatusCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in ResizeShellAsync.", ex);
                throw new Exception($"Unexpected error while resizing shell: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Renames a shell.
        /// </summary>
        /// <param name="request">The <see cref="SandboxShellRenameRequest"/> containing the shell ID and new name.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// A <see cref="SandboxShellSuccessResponse{object}"/> indicating the result of the rename operation.
        /// </returns>
        /// <exception cref="Exception">Thrown if the API call fails or an unexpected error occurs.</exception>
        public async Task<SandboxShellSuccessResponse<object>> RenameShellAsync(SandboxShellRenameRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInfo("Starting RenameShellAsync...");
            try
            {
                var response = await _client.PostAsync<SandboxShellSuccessResponse<object>>("/shell/rename", request, cancellationToken);
                _logger.LogSuccess("RenameShellAsync completed successfully.");
                return response;
            }
            catch (ApiException ex)
            {
                _logger.LogError($"API error in RenameShellAsync: {ex.Message} (Status: {ex.StatusCode})");
                throw new Exception($"API error while renaming shell: {ex.Message} (Status: {ex.StatusCode})", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in RenameShellAsync.", ex);
                throw new Exception($"Unexpected error while renaming shell: {ex.Message}", ex);
            }
        }
    }
}