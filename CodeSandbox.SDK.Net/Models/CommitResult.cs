namespace CodeSandbox.SDK.Net.Models
{
    public class CommitResult
    {
        public int Status { get; set; }
        public CommitResultData Result { get; set; }
    }

    public class CommitResultData
    {
        public string ShellId { get; set; }
    }
}
