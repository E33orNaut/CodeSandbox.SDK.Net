using System.Collections.Generic;

namespace CodeSandbox.SDK.Net.Models
{
    public class GitDiffStatusResult
    {
        public int Status { get; set; }
        public GitDiffStatusData Result { get; set; }
    }

    public class GitDiffStatusData
    {
        public List<GitDiffStatusItem> Files { get; set; }
    }

    public class GitDiffStatusItem
    {
        public string Status { get; set; } // "", "M", "A", etc.
        public string Path { get; set; }
        public string OldPath { get; set; }
        public List<Hunk> Hunks { get; set; }
    }

    public class Hunk
    {
        public Range Original { get; set; }
        public Range Modified { get; set; }
    }

    public class Range
    {
        public int Start { get; set; }
        public int End { get; set; }
    }
}
