namespace CodeSandbox.SDK.Net.Models
{
    public class GitItem
    {
        public string Path { get; set; }
        public string Index { get; set; } // Enum values: "", "M", "A", "D", etc.
        public string WorkingTree { get; set; }
        public bool IsStaged { get; set; }
        public bool IsConflicted { get; set; }
        public string FileId { get; set; }
    }
}