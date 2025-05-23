using System.Collections.Generic;

namespace Models.New.Git
{
    public class GitTransposeLinesRequestItem
    {
        public string Sha { get; set; }
        public string Path { get; set; }
        public int Line { get; set; }
    }

    public class GitTransposeLinesResponse
    {
        public int Status { get; set; }
        public List<GitTransposeLinesResultItem> Result { get; set; }
    }

    public class GitTransposeLinesResultItem
    {
        public string Path { get; set; }
        public int Line { get; set; }
    }

    public class GitTransposeLinesErrorResponse
    {
        public int Status { get; set; }
        public GitTransposeLinesCommonError Error { get; set; }
    }

    public class GitTransposeLinesCommonError
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}