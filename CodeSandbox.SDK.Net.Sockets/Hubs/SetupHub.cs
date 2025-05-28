using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Services;
using Microsoft.AspNet.SignalR;

namespace CodeSandbox.SDK.Net.Sockets.Hubs
{
    /// <summary>
    /// SignalR hub for managing setup operations in the sandbox.
    /// </summary>
    public class SetupHub : Hub
    {
        private static ApiClient client = new ApiClient(ServerContext.ApiKey);
        private static SetupService service = new SetupService(client);

        /// <summary>
        /// Gets the current setup progress.
        /// </summary>
        public async Task<object> GetSetupProgress()
        {
            try
            {
                var result = await service.GetSetupProgressAsync();
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        /// <summary>
        /// Skips a specific setup step.
        /// </summary>
        public async Task<object> SkipStep(int stepIndexToSkip)
        {
            try
            {
                var result = await service.SkipStepAsync(stepIndexToSkip);
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        /// <summary>
        /// Skips all setup steps.
        /// </summary>
        public async Task<object> SkipAllSteps()
        {
            try
            {
                var result = await service.SkipAllStepsAsync();
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        /// <summary>
        /// Disables the setup process.
        /// </summary>
        public async Task<object> DisableSetup()
        {
            try
            {
                var result = await service.DisableSetupAsync();
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        /// <summary>
        /// Enables the setup process.
        /// </summary>
        public async Task<object> EnableSetup()
        {
            try
            {
                var result = await service.EnableSetupAsync();
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        /// <summary>
        /// Initializes the setup process.
        /// </summary>
        public async Task<object> InitializeSetup()
        {
            try
            {
                var result = await service.InitializeSetupAsync();
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        /// <summary>
        /// Sets the current setup step.
        /// </summary>
        public async Task<object> SetStep(int stepIndex)
        {
            try
            {
                var result = await service.SetStepAsync(stepIndex);
                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }
    }
}
