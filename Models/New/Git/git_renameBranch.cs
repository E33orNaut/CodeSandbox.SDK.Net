namespace Models.New.Git
{
    public class GitRenameBranchRequest
    {
        public string OldBranch { get; set; }
        public string NewBranch { get; set; }
    }

    public class GitRenameBranchResponse
    {
        public int Status { get; set; }
        public object Result { get; set; }
    }

    public class GitRenameBranchErrorResponse
    {
        public int Status { get; set; }
        public GitRenameBranchCommonError Error { get; set; }
    }

    public class GitRenameBranchCommonError
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}