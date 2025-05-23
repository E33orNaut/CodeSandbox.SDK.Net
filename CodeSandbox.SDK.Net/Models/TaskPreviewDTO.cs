using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents a preview of a task, including an optional port and a pull request link.
    /// </summary>
    public class TaskPreviewDTO
    {
        [JsonProperty("port")]
        public int? Port { get; set; }

        [JsonProperty("pr-link")]
        public string PrLink { get; set; }
    }
}