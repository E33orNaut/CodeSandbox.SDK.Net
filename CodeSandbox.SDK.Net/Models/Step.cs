using System.Text.Json.Serialization;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents a step in the setup process.
    /// </summary>
    public class Step
    {
        /// <summary>
        /// Gets or sets the name of the step.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed in this step.
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// Gets or sets the shell ID associated with this step, if any.
        /// This property is ignored when null during JSON serialization.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string ShellId { get; set; }

        /// <summary>
        /// Gets or sets the finish status of the step, if available.
        /// This property is ignored when null during JSON serialization.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SetupShellStatus? FinishStatus { get; set; }
    }
}
