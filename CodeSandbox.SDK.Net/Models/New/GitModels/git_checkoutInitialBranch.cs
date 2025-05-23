namespace Codesandbox.SDK.Net.Models.New.Git
{
    public class GitCheckoutInitialBranchResponse
    {
        public int Status { get; set; }
        public object Result { get; set; }
    }

    public class GitCheckoutInitialBranchErrorResponse
    {
        public int Status { get; set; }
        public GitCheckoutInitialBranchCommonError Error { get; set; }
    }

    public class GitCheckoutInitialBranchCommonError
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}