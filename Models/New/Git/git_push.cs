namespace Models.New.Git
{
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
}