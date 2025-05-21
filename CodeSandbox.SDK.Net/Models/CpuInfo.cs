using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models
{
    public class CpuInfo
    {
        [JsonProperty("cores")]
        public int Cores { get; set; }

        [JsonProperty("used")]
        public double Used { get; set; }

        [JsonProperty("configured")]
        public double Configured { get; set; }
    }
}
