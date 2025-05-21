using System.Text.Json.Serialization;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents a successful API response.
    /// </summary>
    public class SuccessResponse
    {
        /// <summary>
        /// Gets or sets the status code of the response.
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// Gets or sets the result object returned by the API.
        /// </summary>
        public object Result { get; set; }
    }

    /// <summary>
    /// Represents short status codes used in Git for file changes.
    /// </summary>
    public enum GitStatusShortFormat
    {
        /// <summary>
        /// No status.
        /// </summary>
        [JsonPropertyName("")]
        None,

        /// <summary>
        /// Modified file.
        /// </summary>
        [JsonPropertyName("M")]
        Modified,

        /// <summary>
        /// Added file.
        /// </summary>
        [JsonPropertyName("A")]
        Added,

        /// <summary>
        /// Deleted file.
        /// </summary>
        [JsonPropertyName("D")]
        Deleted,

        /// <summary>
        /// Renamed file.
        /// </summary>
        [JsonPropertyName("R")]
        Renamed,

        /// <summary>
        /// Copied file.
        /// </summary>
        [JsonPropertyName("C")]
        Copied,

        /// <summary>
        /// Unmerged file (conflicts).
        /// </summary>
        [JsonPropertyName("U")]
        Unmerged,

        /// <summary>
        /// Untracked file.
        /// </summary>
        [JsonPropertyName("?")]
        Untracked
    }

    /// <summary>
    /// Parameters used to specify a Git remote.
    /// </summary>
    public class GitRemoteParams
    {
        /// <summary>
        /// Gets or sets the reference name of the remote.
        /// </summary>
        [JsonPropertyName("reference")]
        public string Reference { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the path (URL) of the remote.
        /// </summary>
        [JsonPropertyName("path")]
        public string Path { get; set; } = string.Empty;
    }

    /// <summary>
    /// Parameters used to specify a Git diff status query.
    /// </summary>
    public class GitDiffStatusParams
    {
        /// <summary>
        /// Gets or sets the base commit or branch for the diff.
        /// </summary>
        [JsonPropertyName("base")]
        public string Base { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the head commit or branch for the diff.
        /// </summary>
        [JsonPropertyName("head")]
        public string Head { get; set; } = string.Empty;
    }
}
