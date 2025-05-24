using System.Collections.Generic;

namespace CodeSandbox.SDK.Net.Models.New.GitModels
{
    public class GitModels
    {
        // --- git_status ---
        public class GitStatusResponse
        {
            public int Status { get; set; }
            public GitStatusResult Result { get; set; }
        }

        public class GitStatusResult
        {
            public Dictionary<string, GitStatusChangedFile> ChangedFiles { get; set; }
            public List<GitStatusGitItem> DeletedFiles { get; set; }
            public bool Conflicts { get; set; }
            public bool LocalChanges { get; set; }
            public GitStatusBranchProperties Remote { get; set; }
            public GitStatusBranchProperties Target { get; set; }
            public string Head { get; set; }
            public List<GitStatusCommit> Commits { get; set; }
            public string Branch { get; set; }
            public bool IsMerging { get; set; }
        }

        public class GitStatusChangedFile : GitStatusGitItem { }

        public class GitStatusGitItem
        {
            public string Path { get; set; }
            public GitStatusShortFormat Index { get; set; }
            public GitStatusShortFormat WorkingTree { get; set; }
            public bool IsStaged { get; set; }
            public bool IsConflicted { get; set; }
            public string FileId { get; set; }
        }

        public enum GitStatusShortFormat
        {
            None,
            M,
            A,
            D,
            R,
            C,
            U,
            Unknown
        }

        public class GitStatusBranchProperties
        {
            public string Head { get; set; }
            public string Branch { get; set; }
            public int Ahead { get; set; }
            public int Behind { get; set; }
            public bool Safe { get; set; }
        }

        public class GitStatusCommit
        {
            public string Hash { get; set; }
            public string Date { get; set; }
            public string Message { get; set; }
            public string Author { get; set; }
        }

        public class GitStatusErrorResponse
        {
            public int Status { get; set; }
            public GitStatusCommonError Error { get; set; }
        }

        public class GitStatusCommonError
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public object Data { get; set; }
        }

        // --- git_remotes ---
        public class GitRemotesResponse
        {
            public int Status { get; set; }
            public GitRemotesResult Result { get; set; }
        }

        public class GitRemotesResult
        {
            public string Origin { get; set; }
            public string Upstream { get; set; }
        }

        public class GitRemotesErrorResponse
        {
            public int Status { get; set; }
            public GitRemotesCommonError Error { get; set; }
        }

        public class GitRemotesCommonError
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public object Data { get; set; }
        }

        // --- git_targetDiff ---
        public class GitTargetDiffResponse
        {
            public int Status { get; set; }
            public GitTargetDiffResult Result { get; set; }
        }

        public class GitTargetDiffResult
        {
            public int Ahead { get; set; }
            public int Behind { get; set; }
            public List<GitTargetDiffCommit> Commits { get; set; }
        }

        public class GitTargetDiffCommit
        {
            public string Hash { get; set; }
            public string Date { get; set; }
            public string Message { get; set; }
            public string Author { get; set; }
        }

        public class GitTargetDiffErrorResponse
        {
            public int Status { get; set; }
            public GitTargetDiffCommonError Error { get; set; }
        }

        public class GitTargetDiffCommonError
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public object Data { get; set; }
        }

        // --- git_pull ---
        public class GitPullRequest
        {
            public string Branch { get; set; }
            public bool? Force { get; set; }
        }

        public class GitPullResponse
        {
            public int Status { get; set; }
            public object Result { get; set; }
        }

        public class GitPullErrorResponse
        {
            public int Status { get; set; }
            public GitPullCommonError Error { get; set; }
        }

        public class GitPullCommonError
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public object Data { get; set; }
        }

        // --- git_discard ---
        public class GitDiscardRequest
        {
            public List<string> Paths { get; set; }
        }

        public class GitDiscardResponse
        {
            public int Status { get; set; }
            public GitDiscardResult Result { get; set; }
        }

        public class GitDiscardResult
        {
            public List<string> Paths { get; set; }
        }

        public class GitDiscardErrorResponse
        {
            public int Status { get; set; }
            public GitDiscardCommonError Error { get; set; }
        }

        public class GitDiscardCommonError
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public object Data { get; set; }
        }

        // --- git_commit ---
        public class GitCommitRequest
        {
            public List<string> Paths { get; set; }
            public string Message { get; set; }
            public bool? Push { get; set; }
        }

        public class GitCommitResponse
        {
            public int Status { get; set; }
            public GitCommitResult Result { get; set; }
        }

        public class GitCommitResult
        {
            public string ShellId { get; set; }
        }

        public class GitCommitErrorResponse
        {
            public int Status { get; set; }
            public GitCommitCommonError Error { get; set; }
        }

        public class GitCommitCommonError
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public object Data { get; set; }
        }

        // --- git_push ---
        public class GitPushResponse
        {
            public int Status { get; set; }
            public object Result { get; set; }
        }

        public class GitPushErrorResponse
        {
            public int Status { get; set; }
            public GitPushCommonError Error { get; set; }
        }

        public class GitPushCommonError
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public object Data { get; set; }
        }

        // --- git_pushToRemote ---
        public class GitPushToRemoteRequest
        {
            public string Url { get; set; }
            public string Branch { get; set; }
            public bool? SquashAllCommits { get; set; }
        }

        public class GitPushToRemoteResponse
        {
            public int Status { get; set; }
            public object Result { get; set; }
        }

        public class GitPushToRemoteErrorResponse
        {
            public int Status { get; set; }
            public GitPushToRemoteCommonError Error { get; set; }
        }

        public class GitPushToRemoteCommonError
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public object Data { get; set; }
        }

        // --- git_renameBranch ---
        public class GitRenameBranchRequest
        {
            public string OldBranch { get; set; }
            public string NewBranch { get; set; }
        }

        public class GitRenameBranchResponse
        {
            public int Status { get; set; }
            public object Result { get; set; }
        }

        public class GitRenameBranchErrorResponse
        {
            public int Status { get; set; }
            public GitRenameBranchCommonError Error { get; set; }
        }

        public class GitRenameBranchCommonError
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public object Data { get; set; }
        }

        // --- git_remoteContent ---
        public class GitRemoteContentRequest
        {
            public string Reference { get; set; }
            public string Path { get; set; }
        }

        public class GitRemoteContentResponse
        {
            public int Status { get; set; }
            public GitRemoteContentResult Result { get; set; }
        }

        public class GitRemoteContentResult
        {
            public string Content { get; set; }
        }

        public class GitRemoteContentErrorResponse
        {
            public int Status { get; set; }
            public GitRemoteContentCommonError Error { get; set; }
        }

        public class GitRemoteContentCommonError
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public object Data { get; set; }
        }

        // --- git_diffStatus ---
        public class GitDiffStatusRequest
        {
            public string Base { get; set; }
            public string Head { get; set; }
        }

        public class GitDiffStatusResponse
        {
            public int Status { get; set; }
            public GitDiffStatusResult Result { get; set; }
        }

        public class GitDiffStatusResult
        {
            public List<GitDiffStatusFile> Files { get; set; }
        }

        public class GitDiffStatusFile
        {
            public GitDiffStatusShortFormat Status { get; set; }
            public string Path { get; set; }
            public string OldPath { get; set; }
            public List<GitDiffStatusHunk> Hunks { get; set; }
        }

        public enum GitDiffStatusShortFormat
        {
            None,
            M,
            A,
            D,
            R,
            C,
            U,
            Unknown
        }

        public class GitDiffStatusHunk
        {
            public GitDiffStatusRange Original { get; set; }
            public GitDiffStatusRange Modified { get; set; }
        }

        public class GitDiffStatusRange
        {
            public int Start { get; set; }
            public int End { get; set; }
        }

        public class GitDiffStatusErrorResponse
        {
            public int Status { get; set; }
            public GitDiffStatusCommonError Error { get; set; }
        }

        public class GitDiffStatusCommonError
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public object Data { get; set; }
        }

        // --- git_resetLocalWithRemote ---
        public class GitResetLocalWithRemoteResponse
        {
            public int Status { get; set; }
            public object Result { get; set; }
        }

        public class GitResetLocalWithRemoteErrorResponse
        {
            public int Status { get; set; }
            public GitResetLocalWithRemoteCommonError Error { get; set; }
        }

        public class GitResetLocalWithRemoteCommonError
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public object Data { get; set; }
        }

        // --- git_checkoutInitialBranch ---
        public class GitCheckoutInitialBranchResponse
        {
            public int Status { get; set; }
            public object Result { get; set; }
        }

        public class GitCheckoutInitialBranchErrorResponse
        {
            public int Status { get; set; }
            public GitCheckoutInitialBranchCommonError Error { get; set; }
        }

        public class GitCheckoutInitialBranchCommonError
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public object Data { get; set; }
        }

        // --- git_transposeLines ---
        public class GitTransposeLinesRequestItem
        {
            public string Sha { get; set; }
            public string Path { get; set; }
            public int Line { get; set; }
        }

        public class GitTransposeLinesResponse
        {
            public int Status { get; set; }
            public List<GitTransposeLinesResultItem> Result { get; set; }
        }

        public class GitTransposeLinesResultItem
        {
            public string Path { get; set; }
            public int Line { get; set; }
        }

        public class GitTransposeLinesErrorResponse
        {
            public int Status { get; set; }
            public GitTransposeLinesCommonError Error { get; set; }
        }

        public class GitTransposeLinesCommonError
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public object Data { get; set; }
        }
    }
}