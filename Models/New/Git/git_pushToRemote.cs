namespace Models.New.Git
{
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
}