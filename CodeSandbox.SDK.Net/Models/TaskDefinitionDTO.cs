using System.Text.Json.Serialization;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents the definition of a task to be run in the setup.
    /// </summary>
    public class TaskDefinitionDTO
    {
        /// <summary>
        /// Gets or sets the name of the task.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the command line string to execute the task.
        /// </summary>
        [JsonPropertyName("command")]
        public string Command { get; set; }

        /// <summary>
        /// Gets or sets whether the task should run automatically at startup.
        /// </summary>
        [JsonPropertyName("runAtStart")]
        public bool? RunAtStart { get; set; }

        /// <summary>
        /// Gets or sets the preview information about the task.
        /// </summary>
        [JsonPropertyName("preview")]
        public TaskPreviewDTO Preview { get; set; }
    }
}
