using System.Collections.Generic;

namespace Models.New.Git
{
    public class GitCommitRequest
    {
        public List<string> Paths { get; set; }
        public string Message { get; set; }
        public bool? Push { get; set; }
    }

    public class GitCommitResponse
    {
        public int Status { get; set; }
        public GitCommitResult Result { get; set; }
    }

    public class GitCommitResult
    {
        public string ShellId { get; set; }
    }

    public class GitCommitErrorResponse
    {
        public int Status { get; set; }
        public GitCommitCommonError Error { get; set; }
    }

    public class GitCommitCommonError
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}