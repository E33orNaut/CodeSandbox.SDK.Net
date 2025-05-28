using System.Threading;
using System.Threading.Tasks;

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
        /// <returns>A task that returns the <see cref="Models.New.PortModels.PortListResult"/> containing the list of ports.</returns>
        Task<Models.New.PortModels.PortListResult> GetPortListAsync(CancellationToken cancellationToken = default);
    }
}
