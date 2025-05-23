using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents the definition of a task to be run in the setup.
    /// </summary>
    public class TaskDefinitionDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("command")]
        public string Command { get; set; }

        [JsonProperty("runAtStart")]
        public bool? RunAtStart { get; set; }

        [JsonProperty("preview")]
        public TaskPreviewDTO Preview { get; set; }
    }
}