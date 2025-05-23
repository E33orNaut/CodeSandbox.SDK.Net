using System.Collections.Generic;

namespace Codesandbox.SDK.Net.Models.New.Git
{
    public class GitDiscardRequest
    {
        public List<string> Paths { get; set; }
    }

    public class GitDiscardResponse
    {
        public int Status { get; set; }
        public GitDiscardResult Result { get; set; }
    }

    public class GitDiscardResult
    {
        public List<string> Paths { get; set; }
    }

    public class GitDiscardErrorResponse
    {
        public int Status { get; set; }
        public GitDiscardCommonError Error { get; set; }
    }

    public class GitDiscardCommonError
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}