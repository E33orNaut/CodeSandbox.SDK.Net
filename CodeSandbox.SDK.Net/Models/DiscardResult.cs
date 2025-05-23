using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents the result of a discard operation.
    /// </summary>
    public class DiscardResult
    {
        /// <summary>
        /// Gets or sets the status code of the discard operation.
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the detailed result data of the discard operation.
        /// </summary>
        [JsonProperty("result")]
        public DiscardResultData Result { get; set; }
    }

    /// <summary>
    /// Contains detailed information about the discard operation result.
    /// </summary>
    public class DiscardResultData
    {
        /// <summary>
        /// Gets or sets the list of file paths that were discarded.
        /// </summary>
        [JsonProperty("paths")]
        public List<string> Paths { get; set; }
    }
}
