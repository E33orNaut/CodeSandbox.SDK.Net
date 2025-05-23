using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models.New
{
    /// <summary>
    /// Request model for POST /git/status.
    /// </summary>
    public class GitStatusRequestModel
    {
        // No properties required for this request.
    }

    /// <summary>
    /// Response model for a successful POST /git/status.
    /// </summary>
    public class GitStatusResponseModel
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("result")]
        public GitStatusResult Result { get; set; }
    }

    /// <summary>
    /// Error response model for POST /git/status.
    /// </summary>
    public class GitStatusErrorResponseModel
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("error")]
        public CommonError Error { get; set; }
    }

    /// <summary>
    /// Represents the result payload for /git/status.
    /// </summary>
    public class GitStatusResult
    {
        [JsonProperty("changedFiles")]
        public Dictionary<string, GitItem> ChangedFiles { get; set; }

        [JsonProperty("deletedFiles")]
        public List<GitItem> DeletedFiles { get; set; }

        [JsonProperty("conflicts")]
        public bool Conflicts { get; set; }

        [JsonProperty("localChanges")]
        public bool LocalChanges { get; set; }

        [JsonProperty("remote")]
        public GitBranchProperties Remote { get; set; }

        [JsonProperty("target")]
        public GitBranchProperties Target { get; set; }

        [JsonProperty("head")]
        public string Head { get; set; }

        [JsonProperty("commits")]
        public List<GitCommit> Commits { get; set; }

        [JsonProperty("branch")]
        public string Branch { get; set; }

        [JsonProperty("isMerging")]
        public bool IsMerging { get; set; }
    }

    public class GitItem
    {
        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("index")]
        public string Index { get; set; }

        [JsonProperty("workingTree")]
        public string WorkingTree { get; set; }

        [JsonProperty("isStaged")]
        public bool IsStaged { get; set; }

        [JsonProperty("isConflicted")]
        public bool IsConflicted { get; set; }

        [JsonProperty("fileId")]
        public string FileId { get; set; }
    }

    public class GitBranchProperties
    {
        [JsonProperty("head")]
        public string Head { get; set; }

        [JsonProperty("branch")]
        public string Branch { get; set; }

        [JsonProperty("ahead")]
        public int Ahead { get; set; }

        [JsonProperty("behind")]
        public int Behind { get; set; }

        [JsonProperty("safe")]
        public bool Safe { get; set; }
    }

    public class GitCommit
    {
        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }
    }

    public class CommonError
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; }
    }
}