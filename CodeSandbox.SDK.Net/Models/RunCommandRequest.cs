using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents a request to run a command in a task.
    /// </summary>
    public class RunCommandRequest
    {
        [JsonProperty("command")]
        public string Command { get; set; }
    }
}
