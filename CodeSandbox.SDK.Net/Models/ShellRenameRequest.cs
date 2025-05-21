using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents a request to rename a shell.
    /// </summary>
    public class ShellRenameRequest
    {
        /// <summary>
        /// Gets or sets the ID of the shell to rename.
        /// </summary>
        [JsonProperty("shellId")]
        public string ShellId { get; set; }

        /// <summary>
        /// Gets or sets the new name for the shell.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
