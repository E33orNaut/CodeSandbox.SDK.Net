using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents the result of a Git diff status operation.
    /// </summary>
    public class GitDiffStatusResult
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("result")]
        public GitDiffStatusData Result { get; set; }
    }

    /// <summary>
    /// Contains detailed information about the Git diff status.
    /// </summary>
    public class GitDiffStatusData
    {
        [JsonProperty("files")]
        public List<GitDiffStatusItem> Files { get; set; }
    }

    /// <summary>
    /// Represents the diff status information for a single file.
    /// </summary>
    public class GitDiffStatusItem
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("oldPath")]
        public string OldPath { get; set; }

        [JsonProperty("hunks")]
        public List<Hunk> Hunks { get; set; }
    }

    /// <summary>
    /// Represents a hunk of changes in a diff.
    /// </summary>
    public class Hunk
    {
        [JsonProperty("original")]
        public Range Original { get; set; }

        [JsonProperty("modified")]
        public Range Modified { get; set; }
    }

    /// <summary>
    /// Represents a range of lines in a file.
    /// </summary>
    public class Range
    {
        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonProperty("end")]
        public int End { get; set; }
    }
}
