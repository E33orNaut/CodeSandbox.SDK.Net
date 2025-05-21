using System.Collections.Generic;

namespace CodeSandbox.SDK.Net.Models
{
    public class GitTargetDiff
    {
        public int Ahead { get; set; }
        public int Behind { get; set; }
        public List<GitCommit> Commits { get; set; }
    }
}
