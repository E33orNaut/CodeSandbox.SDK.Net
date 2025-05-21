namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents the status of a setup shell step.
    /// </summary>
    public enum SetupShellStatus
    {
        /// <summary>
        /// The step completed successfully.
        /// </summary>
        SUCCEEDED,

        /// <summary>
        /// The step failed.
        /// </summary>
        FAILED,

        /// <summary>
        /// The step was skipped.
        /// </summary>
        SKIPPED
    }
}
