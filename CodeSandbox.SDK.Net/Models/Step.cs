    using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents a single step in a setup or task process.
    /// </summary>
    public class Step
    {
        /// <summary>
        /// Gets or sets the name of the step.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the status of the step.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the index of the step.
        /// </summary>
        [JsonProperty("index")]
        public int Index { get; set; }
    }
}
