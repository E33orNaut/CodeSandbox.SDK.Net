using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents a system error returned from the API.
    /// </summary>
    public class SystemError
    {
        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets additional error data, if any.
        /// </summary>
        [JsonProperty("data")]
        public object Data { get; set; }
    }
}
