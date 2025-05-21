namespace CodeSandbox.SDK.Net.Models
{
    public class GitBranchProperties
    {
        public string Head { get; set; }
        public string Branch { get; set; }
        public int Ahead { get; set; }
        public int Behind { get; set; }
        public bool Safe { get; set; }
    }
}