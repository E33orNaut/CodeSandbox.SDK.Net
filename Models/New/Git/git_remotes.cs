namespace Codesandbox.SDK.Net.Models.New.Git
{
    public class GitRemotesResponse
    {
        public int Status { get; set; }
        public GitRemotesResult Result { get; set; }
    }

    public class GitRemotesResult
    {
        public string Origin { get; set; }
        public string Upstream { get; set; }
    }

    public class GitRemotesErrorResponse
    {
        public int Status { get; set; }
        public GitRemotesCommonError Error { get; set; }
    }

    public class GitRemotesCommonError
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}