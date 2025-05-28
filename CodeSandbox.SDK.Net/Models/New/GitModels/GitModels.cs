using System.Collections.Generic;

namespace CodeSandbox.SDK.Net.Models.New.GitModels
{
    /// <summary>
    ///  
    /// </summary>
    public class GitRemotesResult
    {

        /// <summary>
        /// }
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Upstream { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class GitTargetDiffResult
    {
        public int Ahead { get; set; }
        public int Behind { get; set; }
        public List<GitCommit> Commits { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class GitStatusResult
    {
        public GitChangedFiles ChangedFiles { get; set; }
        public List<GitItem> DeletedFiles { get; set; }
        public bool Conflicts { get; set; }
        public bool LocalChanges { get; set; }
        public GitBranchProperties Remote { get; set; }
        public GitBranchProperties Target { get; set; }
        public string Head { get; set; }
        public List<GitCommit> Commits { get; set; }
        public string Branch { get; set; }
        public bool IsMerging { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SuccessResponse<T>
    {
        public int Status { get; set; } = 0;
        public T Result { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ErrorResponse<T>
    {
        public int Status { get; set; } = 1;
        public T Error { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class CommonError
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
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
    /// <summary>
    /// 
    /// </summary>
    public class GitItem
    {
        public string Path { get; set; }
        public GitStatusShortFormat Index { get; set; }
        public GitStatusShortFormat WorkingTree { get; set; }
        public bool IsStaged { get; set; }
        public bool IsConflicted { get; set; }
        public string FileId { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class GitChangedFiles : Dictionary<string, GitItem> { }
    /// <summary>
    /// 
    /// </summary>
    public class GitBranchProperties
    {
        public string Head { get; set; }
        public string Branch { get; set; }
        public int Ahead { get; set; }
        public int Behind { get; set; }
        public bool Safe { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class GitCommit
    {
        public string Hash { get; set; }
        public string Date { get; set; }
        public string Message { get; set; }
        public string Author { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class GitStatus
    {
        public GitChangedFiles ChangedFiles { get; set; }
        public List<GitItem> DeletedFiles { get; set; }
        public bool Conflicts { get; set; }
        public bool LocalChanges { get; set; }
        public GitBranchProperties Remote { get; set; }
        public GitBranchProperties Target { get; set; }
        public string Head { get; set; }
        public List<GitCommit> Commits { get; set; }
        public string Branch { get; set; }
        public bool IsMerging { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class GitTargetDiff
    {
        public int Ahead { get; set; }
        public int Behind { get; set; }
        public List<GitCommit> Commits { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class GitRemotes
    {
        public string Origin { get; set; }
        public string Upstream { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class GitRemoteParams
    {
        public string Reference { get; set; }
        public string Path { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class GitDiffStatusParams
    {
        public string Base { get; set; }
        public string Head { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class GitHunkRange
    {
        public int Start { get; set; }
        public int End { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class GitHunk
    {
        public GitHunkRange Original { get; set; }
        public GitHunkRange Modified { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class GitDiffStatusItem
    {
        public GitStatusShortFormat Status { get; set; }
        public string Path { get; set; }
        public string OldPath { get; set; }
        public List<GitHunk> Hunks { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class GitDiffStatusResult
    {
        public List<GitDiffStatusItem> Files { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class GitDiscardRequest
    {
        public List<string> Paths { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class GitDiscardResult
    {
        public List<string> Paths { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class GitCommitRequest
    {
        public List<string> Paths { get; set; }
        public string Message { get; set; }
        public bool? Push { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>

    public class GitCommitResult
    {
        public string ShellId { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class GitPushToRemoteRequest
    {
        public string Url { get; set; }
        public string Branch { get; set; }
        public bool? SquashAllCommits { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class GitRenameBranchRequest
    {
        public string OldBranch { get; set; }
        public string NewBranch { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class GitRemoteContentResult
    {
        public string Content { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class GitTransposeLinesRequestItem
    {
        public string Sha { get; set; }
        public string Path { get; set; }
        public int Line { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class GitTransposeLinesResultItem
    {
        public string Path { get; set; }
        public int Line { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    // Response wrappers
    public class GitStatusResponse
    {
        public int Status { get; set; }
        public GitStatus Result { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class GitRemotesResponse
    {
        public int Status { get; set; }
        public GitRemotes Result { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class GitTargetDiffResponse
    {
        public int Status { get; set; }
        public GitTargetDiff Result { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class GitDiscardResponse
    {
        public int Status { get; set; }
        public GitDiscardResult Result { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class GitCommitResponse
    {
        public int Status { get; set; }
        public GitCommitResult Result { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class GitRemoteContentResponse
    {
        public int Status { get; set; }
        public GitRemoteContentResult Result { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class GitDiffStatusResponse
    {
        public int Status { get; set; }
        public GitDiffStatusResult Result { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class GitTransposeLinesResponse
    {
        public int Status { get; set; }
        public List<GitTransposeLinesResultItem> Result { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class EmptyResponse
    {
        public int Status { get; set; }
        public object Result { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ErrorResponseCommon
    {
        public int Status { get; set; }
        public CommonError Error { get; set; }
    }
}
