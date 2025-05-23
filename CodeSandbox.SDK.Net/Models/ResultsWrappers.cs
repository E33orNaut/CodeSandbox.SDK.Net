using Newtonsoft.Json;

    namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents the response containing the Git status information.
    /// </summary>
    public class GitStatusResult
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("result")]
        public GitStatus Result { get; set; }
    }

    /// <summary>
    /// Represents the response containing Git remote repository information.
    /// </summary>
    public class GitRemotesResult
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("result")]
        public GitRemotes Result { get; set; }
    }

    /// <summary>
    /// Represents the response containing Git target diff information.
    /// </summary>
    public class GitTargetDiffResult
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("result")]
        public GitTargetDiff Result { get; set; }
    }

    /// <summary>
    /// Represents an error response with status and error details.
    /// </summary>
    public class ErrorResult
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("error")]
        public CommonError Error { get; set; }
    }
}
