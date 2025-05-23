using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents system metrics status information.
    /// </summary>
    public class SystemMetricsStatus
    {
        /// <summary>
        /// Gets or sets CPU usage information.
        /// </summary>
        [JsonProperty("cpu")]
        public CpuInfo Cpu { get; set; }

        /// <summary>
        /// Gets or sets memory usage information.
        /// </summary>
        [JsonProperty("memory")]
        public MemoryInfo Memory { get; set; }

        /// <summary>
        /// Gets or sets storage usage information.
        /// </summary>
        [JsonProperty("storage")]
        public StorageInfo Storage { get; set; }
    }
}
