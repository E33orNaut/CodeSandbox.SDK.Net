using System.Collections.Generic;

namespace Codesandbox.SDK.Net.Models.New.Git
{
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
}