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
        /// Retrieves the list of ports asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The port list response.</returns>
        Task<PortListResponse> GetPortListAsync(CancellationToken cancellationToken = default);
    }
}
