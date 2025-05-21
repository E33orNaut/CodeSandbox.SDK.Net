using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Models;

namespace CodeSandbox.SDK.Net.Interfaces
{
    /// <summary>
    /// Interface for retrieving port information.
    /// </summary>
    public interface IPortService
    {
        /// <summary>
        /// Asynchronously retrieves the list of ports.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that returns the <see cref="PortListResponse"/> containing the list of ports.</returns>
        Task<PortListResponse> GetPortListAsync(CancellationToken cancellationToken = default);
    }
}
