using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models
{
    public class SystemMetricsStatus
    {
        [JsonProperty("cpu")]
        public CpuInfo Cpu { get; set; }

        [JsonProperty("memory")]
        public MemoryInfo Memory { get; set; }

        [JsonProperty("storage")]
        public StorageInfo Storage { get; set; }
    }
}
