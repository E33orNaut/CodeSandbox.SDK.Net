using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Models.New.SandboxSetupModels;

namespace CodeSandbox.SDK.Net.Interfaces
{
    /// <summary>
    /// Interface for managing setup-related API endpoints.
    /// </summary>
    public interface ISetupService
    {
        /// <summary>
        /// Asynchronously gets the current setup progress.
        /// </summary>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A task returning the current setup progress.</returns>
        Task<SandboxSetupSuccessResponse> GetSetupProgressAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously skips a specific setup step.
        /// </summary>
        /// <param name="stepIndexToSkip">The zero-based index of the step to skip.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A task returning the updated setup progress after skipping the step.</returns>
        Task<SandboxSetupSuccessResponse> SkipStepAsync(int stepIndexToSkip, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously skips all remaining setup steps.
        /// </summary>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A task returning the updated setup progress after skipping all steps.</returns>
        Task<SandboxSetupSuccessResponse> SkipAllStepsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously disables the setup process.
        /// </summary>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A task returning the updated setup progress after disabling setup.</returns>
        Task<SandboxSetupSuccessResponse> DisableSetupAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously enables the setup process.
        /// </summary>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A task returning the updated setup progress after enabling setup.</returns>
        Task<SandboxSetupSuccessResponse> EnableSetupAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously initializes the setup process.
        /// </summary>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A task returning the updated setup progress after initialization.</returns>
        Task<SandboxSetupSuccessResponse> InitializeSetupAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously sets the current step of the setup process.
        /// </summary>
        /// <param name="stepIndex">The zero-based index of the step to set as current.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A task returning the updated setup progress after setting the step.</returns>
        Task<SandboxSetupSuccessResponse> SetStepAsync(int stepIndex, CancellationToken cancellationToken = default);
    }
}
