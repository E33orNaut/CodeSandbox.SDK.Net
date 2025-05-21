using System.Text.Json.Serialization;

namespace CodeSandbox.SDK.Net.Models
{
    public class TaskDefinitionDTO
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("command")]
        public string Command { get; set; }

        [JsonPropertyName("runAtStart")]
        public bool? RunAtStart { get; set; }

        [JsonPropertyName("preview")]
        public TaskPreviewDTO Preview { get; set; }
    }

}
