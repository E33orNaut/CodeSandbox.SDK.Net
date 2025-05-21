using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Models;

namespace CodeSandbox.SDK.Net.Interfaces
{
    /// <summary>
    /// Interface for shell-related API operations.
    /// </summary>
    public interface IShellService
    {
        /// <summary>
        /// Renames a shell asynchronously.
        /// </summary>
        /// <param name="request">The request containing the shell rename details.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the success response from the API.</returns>
        Task<SuccessResponse> RenameShellAsync(ShellRenameRequest request, CancellationToken cancellationToken = default);
    }
}
