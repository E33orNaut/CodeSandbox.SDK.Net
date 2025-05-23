namespace Models.New.Git
{
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
}