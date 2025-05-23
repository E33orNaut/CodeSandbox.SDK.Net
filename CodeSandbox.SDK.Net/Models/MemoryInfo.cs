using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents memory usage information.
    /// </summary>
    public class MemoryInfo
    {
        [JsonProperty("used")]
        public long Used { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("configured")]
        public long Configured { get; set; }
    }
}
