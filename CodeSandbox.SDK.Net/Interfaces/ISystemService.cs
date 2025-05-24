using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Models.New.SandboxSystemModels;

namespace CodeSandbox.SDK.Net.Interfaces
{
    /// <summary>
    /// Interface for system-related API operations.
    /// </summary>
    public interface ISystemService
    {
        /// <summary>
        /// Sends a request to update the system asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A <see cref="SandboxSystemSuccessResponse"/> indicating the result.</returns>
        Task<SandboxSystemSuccessResponse> UpdateSystemAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a request to hibernate the system asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A <see cref="SandboxSystemSuccessResponse"/> indicating the result.</returns>
        Task<SandboxSystemSuccessResponse> HibernateSystemAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves system metrics asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A <see cref="SandboxSystemMetricsStatus"/> containing system metrics.</returns>
        Task<SandboxSystemMetricsStatus> GetSystemMetricsAsync(CancellationToken cancellationToken = default);
    }
}
