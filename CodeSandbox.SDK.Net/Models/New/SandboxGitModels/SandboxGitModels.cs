using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CodeSandbox.SDK.Net.Models.New.SandboxGitModels
{
    // --- Common Response Models ---

    /// <summary>
    /// Represents a successful response from the Git API.
    /// </summary>
    public class SandboxGitSuccessResponse
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
        public object Result { get; set; }
    }

    /// <summary>
    /// Represents an error response from the Git API.
    /// </summary>
    public class SandboxGitErrorResponse
    {
        /// <summary>
        /// Status code for error operations.
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        /// Error details.
        /// </summary>
        [JsonProperty("error")]
        public SandboxGitCommonError Error { get; set; }
    }

    // --- Common Error Models ---

    /// <summary>
    /// Represents common error details for the Git API.
    /// </summary>
    public class SandboxGitCommonError
    {
        /// <summary>
        /// Error code (string for protocol or git error).
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// Error message.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Additional error data, if any (only present for protocol errors).
        /// </summary>
        [JsonProperty("data")]
        public object Data { get; set; }
    }

    // --- Git Status Short Format Enum ---

    /// <summary>
    /// Git status short format codes.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SandboxGitStatusShortFormat
    {
        [JsonProperty("")]
        None, // ""
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
        Unknown // "?"
    }

    // --- Git Item & Changed Files ---

    /// <summary>
    /// Represents a file or directory in the Git status.
    /// </summary>
    public class SandboxGitItem
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
        public SandboxGitStatusShortFormat Index { get; set; }

        /// <summary>
        /// Working tree status short format.
        /// </summary>
        [JsonProperty("workingTree")]
        public SandboxGitStatusShortFormat WorkingTree { get; set; }

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
    /// Dictionary of changed files, keyed by file ID.
    /// </summary>
    public class SandboxGitChangedFiles : Dictionary<string, SandboxGitItem> { }

    // --- Branch Properties ---

    /// <summary>
    /// Properties of a Git branch.
    /// </summary>
    public class SandboxGitBranchProperties
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

    // --- Commit ---

    /// <summary>
    /// Represents a Git commit.
    /// </summary>
    public class SandboxGitCommit
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

    // --- Git Status ---

    /// <summary>
    /// Represents the status of a Git repository.
    /// </summary>
    public class SandboxGitStatus
    {
        /// <summary>
        /// Map of file IDs to GitItems.
        /// </summary>
        [JsonProperty("changedFiles")]
        public SandboxGitChangedFiles ChangedFiles { get; set; }

        /// <summary>
        /// List of deleted files.
        /// </summary>
        [JsonProperty("deletedFiles")]
        public List<SandboxGitItem> DeletedFiles { get; set; }

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
        public SandboxGitBranchProperties Remote { get; set; }

        /// <summary>
        /// Target branch properties.
        /// </summary>
        [JsonProperty("target")]
        public SandboxGitBranchProperties Target { get; set; }

        /// <summary>
        /// Current HEAD commit.
        /// </summary>
        [JsonProperty("head")]
        public string Head { get; set; }

        /// <summary>
        /// List of commits.
        /// </summary>
        [JsonProperty("commits")]
        public List<SandboxGitCommit> Commits { get; set; }

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

    // --- Git Target Diff ---

    /// <summary>
    /// Represents the difference between the current branch and a target branch.
    /// </summary>
    public class SandboxGitTargetDiff
    {
        /// <summary>
        /// Number of commits ahead of target.
        /// </summary>
        [JsonProperty("ahead")]
        public int Ahead { get; set; }

        /// <summary>
        /// Number of commits behind target.
        /// </summary>
        [JsonProperty("behind")]
        public int Behind { get; set; }

        /// <summary>
        /// List of commits.
        /// </summary>
        [JsonProperty("commits")]
        public List<SandboxGitCommit> Commits { get; set; }
    }

    // --- Git Remotes ---

    /// <summary>
    /// Represents the remote repositories configured for the Git repository.
    /// </summary>
    public class SandboxGitRemotes
    {
        /// <summary>
        /// Origin remote URL.
        /// </summary>
        [JsonProperty("origin")]
        public string Origin { get; set; }

        /// <summary>
        /// Upstream remote URL.
        /// </summary>
        [JsonProperty("upstream")]
        public string Upstream { get; set; }
    }

    // --- Git Remote Params ---

    /// <summary>
    /// Parameters for retrieving remote content.
    /// </summary>
    public class SandboxGitRemoteParams
    {
        /// <summary>
        /// Branch or commit hash.
        /// </summary>
        [JsonProperty("reference")]
        public string Reference { get; set; }

        /// <summary>
        /// File path.
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }
    }

    // --- Git Diff Status Params ---

    /// <summary>
    /// Parameters for retrieving diff status between two references.
    /// </summary>
    public class SandboxGitDiffStatusParams
    {
        /// <summary>
        /// Base reference for diffing.
        /// </summary>
        [JsonProperty("base")]
        public string Base { get; set; }

        /// <summary>
        /// Head reference for diffing.
        /// </summary>
        [JsonProperty("head")]
        public string Head { get; set; }
    }

    // --- Git Diff Status ---

    /// <summary>
    /// Represents a file in the diff status.
    /// </summary>
    public class SandboxGitDiffStatusItem
    {
        /// <summary>
        /// Git status short format.
        /// </summary>
        [JsonProperty("status")]
        public SandboxGitStatusShortFormat Status { get; set; }

        /// <summary>
        /// File path.
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }

        /// <summary>
        /// Original file path (for renames).
        /// </summary>
        [JsonProperty("oldPath")]
        public string OldPath { get; set; }

        /// <summary>
        /// List of hunks.
        /// </summary>
        [JsonProperty("hunks")]
        public List<SandboxGitDiffHunk> Hunks { get; set; }
    }

    /// <summary>
    /// Represents a hunk in the diff status.
    /// </summary>
    public class SandboxGitDiffHunk
    {
        /// <summary>
        /// Original range.
        /// </summary>
        [JsonProperty("original")]
        public SandboxGitDiffRange Original { get; set; }

        /// <summary>
        /// Modified range.
        /// </summary>
        [JsonProperty("modified")]
        public SandboxGitDiffRange Modified { get; set; }
    }

    /// <summary>
    /// Represents a range in the diff status.
    /// </summary>
    public class SandboxGitDiffRange
    {
        /// <summary>
        /// Start line.
        /// </summary>
        [JsonProperty("start")]
        public int Start { get; set; }

        /// <summary>
        /// End line.
        /// </summary>
        [JsonProperty("end")]
        public int End { get; set; }
    }

    /// <summary>
    /// Result of a diff status operation.
    /// </summary>
    public class SandboxGitDiffStatusResult
    {
        /// <summary>
        /// List of files in diff status.
        /// </summary>
        [JsonProperty("files")]
        public List<SandboxGitDiffStatusItem> Files { get; set; }
    }
}