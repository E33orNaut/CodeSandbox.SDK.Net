    using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents an error related to a task operation.
    /// </summary>
    public class TaskError
    {
        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the descriptive error message.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets additional data related to the error.
        /// </summary>
        [JsonProperty("data")]
        public object Data { get; set; }
    }
}
