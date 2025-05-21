using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents CPU information including core count and usage statistics.
    /// </summary>
    public class CpuInfo
    {
        /// <summary>
        /// Gets or sets the number of CPU cores.
        /// </summary>
        [JsonProperty("cores")]
        public int Cores { get; set; }

        /// <summary>
        /// Gets or sets the current CPU usage percentage.
        /// </summary>
        [JsonProperty("used")]
        public double Used { get; set; }

        /// <summary>
        /// Gets or sets the configured CPU percentage.
        /// </summary>
        [JsonProperty("configured")]
        public double Configured { get; set; }
    }
}
