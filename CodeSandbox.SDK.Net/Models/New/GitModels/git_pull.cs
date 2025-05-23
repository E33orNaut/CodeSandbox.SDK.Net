namespace Codesandbox.SDK.Net.Models.New.Git
{
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
}