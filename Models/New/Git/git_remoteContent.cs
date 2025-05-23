namespace Models.New.Git
{
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
}