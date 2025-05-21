using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Models;

namespace CodeSandbox.SDK.Net.Interfaces
{
    /// <summary>
    /// Interface for managing setup-related API endpoints.
    /// </summary>
    public interface ISetupService
    {
        Task<SetupProgress> GetSetupProgressAsync(CancellationToken cancellationToken = default);
        Task<SetupProgress> SkipStepAsync(int stepIndexToSkip, CancellationToken cancellationToken = default);
        Task<SetupProgress> SkipAllStepsAsync(CancellationToken cancellationToken = default);
        Task<SetupProgress> DisableSetupAsync(CancellationToken cancellationToken = default);
        Task<SetupProgress> EnableSetupAsync(CancellationToken cancellationToken = default);
        Task<SetupProgress> InitializeSetupAsync(CancellationToken cancellationToken = default);
        Task<SetupProgress> SetStepAsync(int stepIndex, CancellationToken cancellationToken = default);
    }
}
