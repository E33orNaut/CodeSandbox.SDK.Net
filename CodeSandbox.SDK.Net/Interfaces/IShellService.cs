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
        /// <param name="request">Shell rename request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Success response from the API.</returns>
        Task<SuccessResponse> RenameShellAsync(ShellRenameRequest request, CancellationToken cancellationToken = default);
    }
}
