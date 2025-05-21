using System.Text.Json.Serialization;

namespace CodeSandbox.SDK.Net.Models
{
    public class SuccessResponse
    {
        public int Status { get; set; } = 0;

        public object Result { get; set; }
    }



    public enum GitStatusShortFormat
    {
        [JsonPropertyName("")]
        None,

        [JsonPropertyName("M")]
        Modified,

        [JsonPropertyName("A")]
        Added,

        [JsonPropertyName("D")]
        Deleted,

        [JsonPropertyName("R")]
        Renamed,

        [JsonPropertyName("C")]
        Copied,

        [JsonPropertyName("U")]
        Unmerged,

        [JsonPropertyName("?")]
        Untracked
    }



    public class GitRemoteParams
    {
        [JsonPropertyName("reference")]
        public string Reference { get; set; } = string.Empty;

        [JsonPropertyName("path")]
        public string Path { get; set; } = string.Empty;
    }

    public class GitDiffStatusParams
    {
        [JsonPropertyName("base")]
        public string Base { get; set; } = string.Empty;

        [JsonPropertyName("head")]
        public string Head { get; set; } = string.Empty;
    }

}
