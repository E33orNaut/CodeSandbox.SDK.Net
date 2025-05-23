    using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents the status of a Git repository.
    /// </summary>
    public class GitStatus
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
}
