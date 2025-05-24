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

        /// <summary>
        /// Error response for git status.
        /// </summary>
        public class GitStatusErrorResponse
        {
            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("error")]
            public GitStatusCommonError Error { get; set; }
        }

        /// <summary>
        /// Common error for git status.
        /// </summary>
        public class GitStatusCommonError
        {
            [JsonProperty("code")]
            public int Code { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("data")]
            public object Data { get; set; }
        }

        // --- git_remotes ---

        /// <summary>
        /// Response for git remotes operation.
        /// </summary>
        public class GitRemotesResponse
        {
            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("result")]
            public GitRemotesResult Result { get; set; }
        }

        /// <summary>
        /// Result for git remotes.
        /// </summary>
        public class GitRemotesResult
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

        /// <summary>
        /// Error response for git remotes.
        /// </summary>
        public class GitRemotesErrorResponse
        {
            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("error")]
            public GitRemotesCommonError Error { get; set; }
        }

        /// <summary>
        /// Common error for git remotes.
        /// </summary>
        public class GitRemotesCommonError
        {
            [JsonProperty("code")]
            public int Code { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("data")]
            public object Data { get; set; }
        }

        // --- git_targetDiff ---

        /// <summary>
        /// Response for git target diff operation.
        /// </summary>
        public class GitTargetDiffResponse
        {
            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("result")]
            public GitTargetDiffResult Result { get; set; }
        }

        /// <summary>
        /// Result for git target diff.
        /// </summary>
        public class GitTargetDiffResult
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
            public List<GitTargetDiffCommit> Commits { get; set; }
        }

        /// <summary>
        /// Represents a commit in target diff.
        /// </summary>
        public class GitTargetDiffCommit
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

        /// <summary>
        /// Error response for git target diff.
        /// </summary>
        public class GitTargetDiffErrorResponse
        {
            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("error")]
            public GitTargetDiffCommonError Error { get; set; }
        }

        /// <summary>
        /// Common error for git target diff.
        /// </summary>
        public class GitTargetDiffCommonError
        {
            [JsonProperty("code")]
            public int Code { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("data")]
            public object Data { get; set; }
        }

        // --- git_pull ---

        /// <summary>
        /// Request for git pull operation.
        /// </summary>
        public class GitPullRequest
        {
            /// <summary>
            /// Branch to pull from.
            /// </summary>
            [JsonProperty("branch")]
            public string Branch { get; set; }

            /// <summary>
            /// Force pull.
            /// </summary>
            [JsonProperty("force")]
            public bool? Force { get; set; }
        }

        /// <summary>
        /// Response for git pull operation.
        /// </summary>
        public class GitPullResponse
        {
            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("result")]
            public object Result { get; set; }
        }

        /// <summary>
        /// Error response for git pull.
        /// </summary>
        public class GitPullErrorResponse
        {
            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("error")]
            public GitPullCommonError Error { get; set; }
        }

        /// <summary>
        /// Common error for git pull.
        /// </summary>
        public class GitPullCommonError
        {
            [JsonProperty("code")]
            public int Code { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("data")]
            public object Data { get; set; }
        }

        // --- git_discard ---

        /// <summary>
        /// Request for git discard operation.
        /// </summary>
        public class GitDiscardRequest
        {
            /// <summary>
            /// Paths to discard changes for.
            /// </summary>
            [JsonProperty("paths")]
            public List<string> Paths { get; set; }
        }

        /// <summary>
        /// Response for git discard operation.
        /// </summary>
        public class GitDiscardResponse
        {
            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("result")]
            public GitDiscardResult Result { get; set; }
        }

        /// <summary>
        /// Result for git discard.
        /// </summary>
        public class GitDiscardResult
        {
            /// <summary>
            /// Paths that were discarded.
            /// </summary>
            [JsonProperty("paths")]
            public List<string> Paths { get; set; }
        }

        /// <summary>
        /// Error response for git discard.
        /// </summary>
        public class GitDiscardErrorResponse
        {
            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("error")]
            public GitDiscardCommonError Error { get; set; }
        }

        /// <summary>
        /// Common error for git discard.
        /// </summary>
        public class GitDiscardCommonError
        {
            [JsonProperty("code")]
            public int Code { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("data")]
            public object Data { get; set; }
        }

        // --- git_commit ---

        /// <summary>
        /// Request for git commit operation.
        /// </summary>
        public class GitCommitRequest
        {
            /// <summary>
            /// Paths to commit.
            /// </summary>
            [JsonProperty("paths")]
            public List<string> Paths { get; set; }

            /// <summary>
            /// Commit message.
            /// </summary>
            [JsonProperty("message")]
            public string Message { get; set; }

            /// <summary>
            /// Whether to push after committing.
            /// </summary>
            [JsonProperty("push")]
            public bool? Push { get; set; }
        }

        /// <summary>
        /// Response for git commit operation.
        /// </summary>
        public class GitCommitResponse
        {
            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("result")]
            public GitCommitResult Result { get; set; }
        }

        /// <summary>
        /// Result for git commit.
        /// </summary>
        public class GitCommitResult
        {
            /// <summary>
            /// ID of the shell process.
            /// </summary>
            [JsonProperty("shellId")]
            public string ShellId { get; set; }
        }

        /// <summary>
        /// Error response for git commit.
        /// </summary>
        public class GitCommitErrorResponse
        {
            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("error")]
            public GitCommitCommonError Error { get; set; }
        }

        /// <summary>
        /// Common error for git commit.
        /// </summary>
        public class GitCommitCommonError
        {
            [JsonProperty("code")]
            public int Code { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("data")]
            public object Data { get; set; }
        }

        // --- git_push ---

        /// <summary>
        /// Response for git push operation.
        /// </summary>
        public class GitPushResponse
        {
            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("result")]
            public object Result { get; set; }
        }

        /// <summary>
        /// Error response for git push.
        /// </summary>
        public class GitPushErrorResponse
        {
            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("error")]
            public GitPushCommonError Error { get; set; }
        }

        /// <summary>
        /// Common error for git push.
        /// </summary>
        public class GitPushCommonError
        {
            [JsonProperty("code")]
            public int Code { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("data")]
            public object Data { get; set; }
        }

        // --- git_pushToRemote ---

        /// <summary>
        /// Request for git push to remote operation.
        /// </summary>
        public class GitPushToRemoteRequest
        {
            /// <summary>
            /// URL of the remote repository.
            /// </summary>
            [JsonProperty("url")]
            public string Url { get; set; }

            /// <summary>
            /// Branch to push to.
            /// </summary>
            [JsonProperty("branch")]
            public string Branch { get; set; }

            /// <summary>
            /// Whether to squash all commits before pushing.
            /// </summary>
            [JsonProperty("squashAllCommits")]
            public bool? SquashAllCommits { get; set; }
        }

        /// <summary>
        /// Response for git push to remote operation.
        /// </summary>
        public class GitPushToRemoteResponse
        {
            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("result")]
            public object Result { get; set; }
        }

        /// <summary>
        /// Error response for git push to remote.
        /// </summary>
        public class GitPushToRemoteErrorResponse
        {
            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("error")]
            public GitPushToRemoteCommonError Error { get; set; }
        }

        /// <summary>
        /// Common error for git push to remote.
        /// </summary>
        public class GitPushToRemoteCommonError
        {
            [JsonProperty("code")]
            public int Code { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("data")]
            public object Data { get; set; }
        }

        // --- git_renameBranch ---

        /// <summary>
        /// Request for git rename branch operation.
        /// </summary>
        public class GitRenameBranchRequest
        {
            /// <summary>
            /// Name of the branch to rename.
            /// </summary>
            [JsonProperty("oldBranch")]
            public string OldBranch { get; set; }

            /// <summary>
            /// New name for the branch.
            /// </summary>
            [JsonProperty("newBranch")]
            public string NewBranch { get; set; }
        }

        /// <summary>
        /// Response for git rename branch operation.
        /// </summary>
        public class GitRenameBranchResponse
        {
            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("result")]
            public object Result { get; set; }
        }

        /// <summary>
        /// Error response for git rename branch.
        /// </summary>
        public class GitRenameBranchErrorResponse
        {
            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("error")]
            public GitRenameBranchCommonError Error { get; set; }
        }

        /// <summary>
        /// Common error for git rename branch.
        /// </summary>
        public class GitRenameBranchCommonError
        {
            [JsonProperty("code")]
            public int Code { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("data")]
            public object Data { get; set; }
        }

        // --- git_remoteContent ---

        /// <summary>
        /// Request for git remote content operation.
        /// </summary>
        public class GitRemoteContentRequest
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

        /// <summary>
        /// Response for git remote content operation.
        /// </summary>
        public class GitRemoteContentResponse
        {
            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("result")]
            public GitRemoteContentResult Result { get; set; }
        }

        /// <summary>
        /// Result for git remote content.
        /// </summary>
        public class GitRemoteContentResult
        {
            /// <summary>
            /// Content of the file.
            /// </summary>
            [JsonProperty("content")]
            public string Content { get; set; }
        }

        /// <summary>
        /// Error response for git remote content.
        /// </summary>
        public class GitRemoteContentErrorResponse
        {
            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("error")]
            public GitRemoteContentCommonError Error { get; set; }
        }

        /// <summary>
        /// Common error for git remote content.
        /// </summary>
        public class GitRemoteContentCommonError
        {
            [JsonProperty("code")]
            public int Code { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("data")]
            public object Data { get; set; }
        }

        // --- git_diffStatus ---

        /// <summary>
        /// Request for git diff status operation.
        /// </summary>
        public class GitDiffStatusRequest
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

        /// <summary>
        /// Response for git diff status operation.
        /// </summary>
        public class GitDiffStatusResponse
        {
            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("result")]
            public GitDiffStatusResult Result { get; set; }
        }

        /// <summary>
        /// Result for git diff status.
        /// </summary>
        public class GitDiffStatusResult
        {
            /// <summary>
            /// List of files in diff status.
            /// </summary>
            [JsonProperty("files")]
            public List<GitDiffStatusFile> Files { get; set; }
        }

        /// <summary>
        /// Represents a file in git diff status.
        /// </summary>
        public class GitDiffStatusFile
        {
            /// <summary>
            /// Git status short format.
            /// </summary>
            [JsonProperty("status")]
            public GitDiffStatusShortFormat Status { get; set; }

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
            public List<GitDiffStatusHunk> Hunks { get; set; }
        }

        /// <summary>
        /// Git diff status short format codes.
        /// </summary>
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum GitDiffStatusShortFormat
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
        /// Represents a hunk in git diff status.
        /// </summary>
        public class GitDiffStatusHunk
        {
            /// <summary>
            /// Original range.
            /// </summary>
            [JsonProperty("original")]
            public GitDiffStatusRange Original { get; set; }

            /// <summary>
            /// Modified range.
            /// </summary>
            [JsonProperty("modified")]
            public GitDiffStatusRange Modified { get; set; }
        }

        /// <summary>
        /// Represents a range in git diff status.
        /// </summary>
        public class GitDiffStatusRange
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
        /// Error response for git diff status.
        /// </summary>
        public class GitDiffStatusErrorResponse
        {
            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("error")]
            public GitDiffStatusCommonError Error { get; set; }
        }

        /// <summary>
        /// Common error for git diff status.
        /// </summary>
        public class GitDiffStatusCommonError
        {
            [JsonProperty("code")]
            public int Code { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("data")]
            public object Data { get; set; }
        }

        // --- git_resetLocalWithRemote ---

        /// <summary>
        /// Response for git reset local with remote operation.
        /// </summary>
        public class GitResetLocalWithRemoteResponse
        {
            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("result")]
            public object Result { get; set; }
        }

        /// <summary>
        /// Error response for git reset local with remote.
        /// </summary>
        public class GitResetLocalWithRemoteErrorResponse
        {
            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("error")]
            public GitResetLocalWithRemoteCommonError Error { get; set; }
        }

        /// <summary>
        /// Common error for git reset local with remote.
        /// </summary>
        public class GitResetLocalWithRemoteCommonError
        {
            [JsonProperty("code")]
            public int Code { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("data")]
            public object Data { get; set; }
        }

        // --- git_checkoutInitialBranch ---

        /// <summary>
        /// Response for git checkout initial branch operation.
        /// </summary>
        public class GitCheckoutInitialBranchResponse
        {
            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("result")]
            public object Result { get; set; }
        }

        /// <summary>
        /// Error response for git checkout initial branch.
        /// </summary>
        public class GitCheckoutInitialBranchErrorResponse
        {
            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("error")]
            public GitCheckoutInitialBranchCommonError Error { get; set; }
        }

        /// <summary>
        /// Common error for git checkout initial branch.
        /// </summary>
        public class GitCheckoutInitialBranchCommonError
        {
            [JsonProperty("code")]
            public int Code { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("data")]
            public object Data { get; set; }
        }

        // --- git_transposeLines ---

        /// <summary>
        /// Request item for git transpose lines operation.
        /// </summary>
        public class GitTransposeLinesRequestItem
        {
            /// <summary>
            /// Commit SHA.
            /// </summary>
            [JsonProperty("sha")]
            public string Sha { get; set; }

            /// <summary>
            /// File path.
            /// </summary>
            [JsonProperty("path")]
            public string Path { get; set; }

            /// <summary>
            /// Line number.
            /// </summary>
            [JsonProperty("line")]
            public int Line { get; set; }
        }

        /// <summary>
        /// Response for git transpose lines operation.
        /// </summary>
        public class GitTransposeLinesResponse
        {
            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("result")]
            public List<GitTransposeLinesResultItem> Result { get; set; }
        }

        /// <summary>
        /// Result item for git transpose lines.
        /// </summary>
        public class GitTransposeLinesResultItem
        {
            /// <summary>
            /// File path.
            /// </summary>
            [JsonProperty("path")]
            public string Path { get; set; }

            /// <summary>
            /// Line number.
            /// </summary>
            [JsonProperty("line")]
            public int Line { get; set; }
        }

        /// <summary>
        /// Error response for git transpose lines.
        /// </summary>
        public class GitTransposeLinesErrorResponse
        {
            [JsonProperty("status")]
            public int Status { get; set; }

            [JsonProperty("error")]
            public GitTransposeLinesCommonError Error { get; set; }
        }

        /// <summary>
        /// Common error for git transpose lines.
        /// </summary>
        public class GitTransposeLinesCommonError
        {
            [JsonProperty("code")]
            public int Code { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("data")]
            public object Data { get; set; }
        }
    }
}