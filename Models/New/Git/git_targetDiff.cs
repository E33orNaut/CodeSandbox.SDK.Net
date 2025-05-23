using Codesandbox.SDK.Net.System.Collections.Generic;

namespace Models.New.Git
{
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
}