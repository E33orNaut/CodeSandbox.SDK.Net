using System.Collections.Generic;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents the current state of a setup process.
    /// </summary>
    public enum SetupState
    {
        /// <summary>
        /// Setup is idle and has not started.
        /// </summary>
        IDLE,

        /// <summary>
        /// Setup is currently in progress.
        /// </summary>
        IN_PROGRESS,

        /// <summary>
        /// Setup has finished successfully.
        /// </summary>
        FINISHED,

        /// <summary>
        /// Setup was stopped before completion.
        /// </summary>
        STOPPED
    }

    /// <summary>
    /// Represents the progress of a setup process, including state and steps.
    /// </summary>
    public class SetupProgress
    {
        /// <summary>
        /// Gets or sets the current state of the setup process.
        /// </summary>
        public SetupState State { get; set; }

        /// <summary>
        /// Gets or sets the list of steps involved in the setup process.
        /// </summary>
        public List<Step> Steps { get; set; } = new List<Step>();

        /// <summary>
        /// Gets or sets the index of the current step being executed.
        /// </summary>
        public int CurrentStepIndex { get; set; }
    }
}
