using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Models;

namespace CodeSandbox.SDK.Net.Interfaces
{
    /// <summary>
    /// Interface for managing container-related operations.
    /// </summary>
    public interface IContainerService
    {
        /// <summary>
        /// Sets up a container asynchronously.
        /// </summary>
        /// <param name="request">Container setup request payload.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Container setup response.</returns>
        Task<ContainerSetupResponse> SetupContainerAsync(ContainerSetupRequest request, CancellationToken cancellationToken = default);
    }
}
