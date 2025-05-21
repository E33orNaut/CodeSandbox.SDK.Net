using System.Text.Json.Serialization;

namespace CodeSandbox.SDK.Net.Models
{
    public class Step
    {
        public string Name { get; set; }

        public string Command { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string ShellId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SetupShellStatus? FinishStatus { get; set; }
    }
}
