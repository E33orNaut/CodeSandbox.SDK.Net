using System.Collections.Generic;

namespace CodeSandbox.SDK.Net.Models
{
    public class GitStatus
    {
        public GitChangedFiles ChangedFiles { get; set; }
        public List<GitItem> DeletedFiles { get; set; }
        public bool Conflicts { get; set; }
        public bool LocalChanges { get; set; }
        public GitBranchProperties Remote { get; set; }
        public GitBranchProperties Target { get; set; }
        public string Head { get; set; }
        public List<GitCommit> Commits { get; set; }
        public string Branch { get; set; }
        public bool IsMerging { get; set; }
    }
}
