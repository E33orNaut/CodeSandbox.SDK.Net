using System.Text.Json.Serialization;

namespace CodeSandbox.SDK.Net.Models
{
    public class TaskPreviewDTO
    {
        [JsonPropertyName("port")]
        public int? Port { get; set; }

        [JsonPropertyName("pr-link")]
        public string PrLink { get; set; }  
    }
}
