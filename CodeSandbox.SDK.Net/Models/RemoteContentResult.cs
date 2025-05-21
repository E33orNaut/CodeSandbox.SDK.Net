namespace CodeSandbox.SDK.Net.Models
{
    public class RemoteContentResult
    {
        public int Status { get; set; }
        public RemoteContentData Result { get; set; }
    }

    public class RemoteContentData
    {
        public string Content { get; set; }
    }
}
