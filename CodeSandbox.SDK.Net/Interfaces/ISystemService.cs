using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Models;

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
        /// <returns>A <see cref="SuccessResponse"/> indicating the result.</returns>
        Task<SuccessResponse> UpdateSystemAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a request to hibernate the system asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A <see cref="SuccessResponse"/> indicating the result.</returns>
        Task<SuccessResponse> HibernateSystemAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves system metrics asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A <see cref="SuccessResponse"/> containing system metrics.</returns>
        Task<SuccessResponse> GetSystemMetricsAsync(CancellationToken cancellationToken = default);
    }
}
