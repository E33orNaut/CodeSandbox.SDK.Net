using System.Text.Json.Serialization;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents a preview of a task, including an optional port and a pull request link.
    /// </summary>
    public class TaskPreviewDTO
    {
        /// <summary>
        /// Gets or sets the port number associated with the task preview, if any.
        /// </summary>
        [JsonPropertyName("port")]
        public int? Port { get; set; }

        /// <summary>
        /// Gets or sets the pull request link related to the task preview.
        /// </summary>
        [JsonPropertyName("pr-link")]
        public string PrLink { get; set; }
    }
}
