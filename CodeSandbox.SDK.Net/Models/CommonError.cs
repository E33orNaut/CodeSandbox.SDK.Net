namespace CodeSandbox.SDK.Net.Models
{
    public class CommonError
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; } // Use a concrete type if known
    }
}
