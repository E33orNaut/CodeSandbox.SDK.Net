using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents a task in the system.
    /// </summary>
    public class TaskDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the task.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the current status of the task.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the progress of the task, represented as a value between 0 and 1.
        /// </summary>
        [JsonProperty("progress")]
        public double Progress { get; set; }
    }
}
