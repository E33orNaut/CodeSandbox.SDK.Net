using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents storage usage information.
    /// </summary>
    public class StorageInfo
    {
        /// <summary>
        /// Gets or sets the amount of storage used (in bytes).
        /// </summary>
        [JsonProperty("used")]
        public long Used { get; set; }

        /// <summary>
        /// Gets or sets the total available storage (in bytes).
        /// </summary>
        [JsonProperty("total")]
        public long Total { get; set; }

        /// <summary>
        /// Gets or sets the configured storage capacity (in bytes).
        /// </summary>
        [JsonProperty("configured")]
        public long Configured { get; set; }
    }
}
