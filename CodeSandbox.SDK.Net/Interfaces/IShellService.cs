using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Models.New.SandboxShellModels;

namespace CodeSandbox.SDK.Net.Interfaces
{
    /// <summary>
    /// Interface for shell-related API operations.
    /// </summary>
    public interface IShellService
    {
        /// <summary>
        /// Creates a new shell (terminal or command) in the sandbox.
        /// </summary>
        Task<SandboxShellSuccessResponse<SandboxOpenShellDTO>> CreateShellAsync(SandboxShellCreateRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends input to an active shell.
        /// </summary>
        Task<SandboxShellSuccessResponse<object>> SendInputAsync(SandboxShellInRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves the list of all available shells in the sandbox.
        /// </summary>
        Task<SandboxShellSuccessResponse<SandboxShellListResult>> ListShellsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Opens an existing shell and retrieves its buffer.
        /// </summary>
        Task<SandboxShellSuccessResponse<SandboxOpenShellDTO>> OpenShellAsync(SandboxShellOpenRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Closes a shell without terminating the underlying process.
        /// </summary>
        Task<SandboxShellSuccessResponse<object>> CloseShellAsync(SandboxShellIdRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Restarts an existing shell process.
        /// </summary>
        Task<SandboxShellSuccessResponse<object>> RestartShellAsync(SandboxShellIdRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Terminates a shell and its underlying process.
        /// </summary>
        Task<SandboxShellSuccessResponse<SandboxShellDTO>> TerminateShellAsync(SandboxShellIdRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Resizes a shell.
        /// </summary>
        Task<SandboxShellSuccessResponse<object>> ResizeShellAsync(SandboxShellResizeRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Renames a shell.
        /// </summary>
        Task<SandboxShellSuccessResponse> RenameShellAsync(SandboxShellRenameRequest request, CancellationToken cancellationToken = default);
    }
}
