using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents memory usage information.
    /// </summary>
    public class MemoryInfo
    {
        /// <summary>
        /// Gets or sets the amount of memory currently used (in bytes).
        /// </summary>
        [JsonProperty("used")]
        public long Used { get; set; }

        /// <summary>
        /// Gets or sets the total amount of memory available (in bytes).
        /// </summary>
        [JsonProperty("total")]
        public long Total { get; set; }

        /// <summary>
        /// Gets or sets the amount of memory configured for the system or container (in bytes).
        /// </summary>
        [JsonProperty("configured")]
        public long Configured { get; set; }
    }
}
