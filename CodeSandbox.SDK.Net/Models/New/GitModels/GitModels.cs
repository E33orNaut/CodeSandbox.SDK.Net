using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models.New.GitModels
{
    /// <summary>
    /// Contains models for interacting with the Git API in CodeSandbox.
    /// </summary>
    public class GitModels
    {
        // --- git_status ---

        /// <summary>
        /// Response for git status operation.
        /// </summary>
        public class GitStatusResponse
        {
            /// <summary>
            /// Status code for successful operations.
            /// </summary>
            [JsonProperty("status")]
            public int Status { get; set; }

            /// <summary>
            /// Result payload for the operation.
            /// </summary>
            [JsonProperty("result")]
            public GitStatusResult Result { get; set; }
        }

        /// <summary>
        /// Git status result details.
        /// </summary>
        public class GitStatusResult
        {
            /// <summary>
            /// Map of file IDs to GitItems.
            /// </summary>
            [JsonProperty("changedFiles")]
            public Dictionary<string, GitStatusChangedFile> ChangedFiles { get; set; }

            /// <summary>
            /// List of deleted files.
            /// </summary>
            [JsonProperty("deletedFiles")]
            public List<GitStatusGitItem> DeletedFiles { get; set; }

            /// <summary>
            /// Whether there are remote conflicts.
            /// </summary>
            [JsonProperty("conflicts")]
            public bool Conflicts { get; set; }

            /// <summary>
            /// Whether there are local changes.
            /// </summary>
            [JsonProperty("localChanges")]
            public bool LocalChanges { get; set; }

            /// <summary>
            /// Remote branch properties.
            /// </summary>
            [JsonProperty("remote")]
            public GitStatusBranchProperties Remote { get; set; }

            /// <summary>
            /// Target branch properties.
            /// </summary>
            [JsonProperty("target")]
            public GitStatusBranchProperties Target { get; set; }

            /// <summary>
            /// Current HEAD commit.
            /// </summary>
            [JsonProperty("head")]
            public string Head { get; set; }

            /// <summary>
            /// List of commits.
            /// </summary>
            [JsonProperty("commits")]
            public List<GitStatusCommit> Commits { get; set; }

            /// <summary>
            /// Current branch name.
            /// </summary>
            [JsonProperty("branch")]
            public string Branch { get; set; }

            /// <summary>
            /// Whether a merge is in progress.
            /// </summary>
            [JsonProperty("isMerging")]
            public bool IsMerging { get; set; }
        }

        /// <summary>
        /// Represents a changed file in git status.
        /// </summary>
        public class GitStatusChangedFile : GitStatusGitItem { }

        /// <summary>
        /// Represents a git item (file) in status.
        /// </summary>
        public class GitStatusGitItem
        {
            /// <summary>
            /// File path.
            /// </summary>
            [JsonProperty("path")]
            public string Path { get; set; }

            /// <summary>
            /// Index status short format.
            /// </summary>
            [JsonProperty("index")]
            public GitStatusShortFormat Index { get; set; }

            /// <summary>
            /// Working tree status short format.
            /// </summary>
            [JsonProperty("workingTree")]
            public GitStatusShortFormat WorkingTree { get; set; }

            /// <summary>
            /// Whether the file is staged.
            /// </summary>
            [JsonProperty("isStaged")]
            public bool IsStaged { get; set; }

            /// <summary>
            /// Whether the file has conflicts.
            /// </summary>
            [JsonProperty("isConflicted")]
            public bool IsConflicted { get; set; }

            /// <summary>
            /// File ID.
            /// </summary>
            [JsonProperty("fileId")]
            public string FileId { get; set; }
        }

        /// <summary>
        /// Git status short format codes.
        /// </summary>
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum GitStatusShortFormat
        {
            [JsonProperty("")]
            None,
            [JsonProperty("M")]
            M,
            [JsonProperty("A")]
            A,
            [JsonProperty("D")]
            D,
            [JsonProperty("R")]
            R,
            [JsonProperty("C")]
            C,
            [JsonProperty("U")]
            U,
            [JsonProperty("?")]
            Unknown
        }

        /// <summary>
        /// Properties of a git branch.
        /// </summary>
        public class GitStatusBranchProperties
        {
            /// <summary>
            /// Head commit.
            /// </summary>
            [JsonProperty("head")]
            public string Head { get; set; }

            /// <summary>
            /// Branch name.
            /// </summary>
            [JsonProperty("branch")]
            public string Branch { get; set; }

            /// <summary>
            /// Number of commits ahead.
            /// </summary>
            [JsonProperty("ahead")]
            public int Ahead { get; set; }

            /// <summary>
            /// Number of commits behind.
            /// </summary>
            [JsonProperty("behind")]
            public int Behind { get; set; }

            /// <summary>
            /// Whether the branch is safe to use.
            /// </summary>
            [JsonProperty("safe")]
            public bool Safe { get; set; }
        }

        /// <summary>
        /// Represents a git commit.
        /// </summary>
        public class GitStatusCommit
        {
            /// <summary>
            /// Commit hash.
            /// </summary>
            [JsonProperty("hash")]
            public string Hash { get; set; }

            /// <summary>
            /// Commit date.
            /// </summary>
            [JsonProperty("date")]
            public string Date { get; set; }

            /// <summary>
            /// Commit message.
            /// </summary>
            [JsonProperty("message")]
            public string Message { get; set; }

            /// <summary>
            /// Commit author.
            /// </summary>
            [JsonProperty("author")]
            public string Author { get; set; }
        }

        // ... (rest of the file unchanged, as your XML and JsonProperty attributes are already correct and consistent)
    }
}