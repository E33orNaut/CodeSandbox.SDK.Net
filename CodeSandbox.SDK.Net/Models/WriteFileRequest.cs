namespace CodeSandbox.SDK.Models
{
    public class WriteFileRequest
    {
        public string Path { get; set; }
        public string Content { get; set; }
        public bool? IsBinary { get; set; }
    }
}
