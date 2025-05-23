using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents the response containing remote content data.
    /// </summary>
    public class RemoteContentResult
    {
        /// <summary>
        /// Gets or sets the status code of the response.
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the result data containing the remote content.
        /// </summary>
        [JsonProperty("result")]
        public RemoteContentData Result { get; set; }
    }

    /// <summary>
    /// Represents the remote content data.
    /// </summary>
    public class RemoteContentData
    {
        /// <summary>
        /// Gets or sets the content retrieved remotely as a string.
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
