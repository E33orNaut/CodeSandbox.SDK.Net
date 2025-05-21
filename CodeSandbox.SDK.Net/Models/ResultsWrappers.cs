namespace CodeSandbox.SDK.Net.Models
{
    public class GitStatusResult
    {
        public int Status { get; set; }
        public GitStatus Result { get; set; }
    }

    public class GitRemotesResult
    {
        public int Status { get; set; }
        public GitRemotes Result { get; set; }
    }

    public class GitTargetDiffResult
    {
        public int Status { get; set; }
        public GitTargetDiff Result { get; set; }
    }

    public class ErrorResult
    {
        public int Status { get; set; }
        public CommonError Error { get; set; }
    }
}
