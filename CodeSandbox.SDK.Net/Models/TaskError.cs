using System.Text.Json.Serialization;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents an error related to a task, including a code and a message.
    /// </summary>
    public class TaskError
    {
        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the descriptive error message.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
