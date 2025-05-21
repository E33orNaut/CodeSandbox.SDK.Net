using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models
{
    public class ShellRenameRequest
    {
        [JsonProperty("shellId")]
        public string ShellId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
