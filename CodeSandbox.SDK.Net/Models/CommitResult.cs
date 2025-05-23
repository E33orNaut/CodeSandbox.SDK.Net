using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents the response from a commit operation.
    /// </summary>
    public class CommitResult
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("result")]
        public CommitResultData Result { get; set; }
    }

    /// <summary>
    /// Contains detailed data returned after a commit operation.
    /// </summary>
    public class CommitResultData
    {
        [JsonProperty("shellId")]
        public string ShellId { get; set; }
    }
}
