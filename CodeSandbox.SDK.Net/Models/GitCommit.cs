using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents a Git commit.
    /// </summary>
    public class GitCommit
    {
        /// <summary>
        /// Gets or sets the commit hash.
        /// </summary>
        [JsonProperty("hash")]
        public string Hash { get; set; }

        /// <summary>
        /// Gets or sets the commit date as a string.
        /// </summary>
        [JsonProperty("date")]
        public string Date { get; set; }

        /// <summary>
        /// Gets or sets the commit message.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the author of the commit.
        /// </summary>
        [JsonProperty("author")]
        public string Author { get; set; }
    }
}
