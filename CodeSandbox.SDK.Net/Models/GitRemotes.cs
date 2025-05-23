using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents the remote repositories configured for a Git repository.
    /// </summary>
    public class GitRemotes
    {
        /// <summary>
        /// Gets or sets the URL of the 'origin' remote.
        /// </summary>
        [JsonProperty("origin")]
        public string Origin { get; set; }

        /// <summary>
        /// Gets or sets the URL of the 'upstream' remote.
        /// </summary>
        [JsonProperty("upstream")]
        public string Upstream { get; set; }
    }
}
