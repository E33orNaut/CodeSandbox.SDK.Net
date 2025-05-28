using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Models.New.SandboxContainerModels;

namespace CodeSandbox.SDK.Net.Interfaces
{
    /// <summary>
    /// Defines operations related to managing containers.
    /// </summary>
    public interface IContainerService
    {
        /// <summary>
        /// Asynchronously sets up a container using the specified setup request.
        /// </summary>
        /// <param name="request">The container setup request containing configuration details.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation, with a <see cref="ContainerSetupResponse"/> result containing setup details.</returns>
        Task<ContainerSetupSuccessResponse> SetupContainerAsync(ContainerSetupRequest request, CancellationToken cancellationToken = default);
    }
}
