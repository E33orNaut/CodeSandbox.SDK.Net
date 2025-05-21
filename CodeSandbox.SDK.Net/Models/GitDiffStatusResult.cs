using System.Collections.Generic;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents the result of a Git diff status operation.
    /// </summary>
    public class GitDiffStatusResult
    {
        /// <summary>
        /// Gets or sets the status code of the response.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the detailed result data.
        /// </summary>
        public GitDiffStatusData Result { get; set; }
    }

    /// <summary>
    /// Contains detailed information about the Git diff status.
    /// </summary>
    public class GitDiffStatusData
    {
        /// <summary>
        /// Gets or sets the list of changed files and their diff details.
        /// </summary>
        public List<GitDiffStatusItem> Files { get; set; }
    }

    /// <summary>
    /// Represents the diff status information for a single file.
    /// </summary>
    public class GitDiffStatusItem
    {
        /// <summary>
        /// Gets or sets the file status code.
        /// Examples include "", "M" (modified), "A" (added), etc.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the current file path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the old file path (if renamed or moved).
        /// </summary>
        public string OldPath { get; set; }

        /// <summary>
        /// Gets or sets the list of hunks (sections of changes) in the file.
        /// </summary>
        public List<Hunk> Hunks { get; set; }
    }

    /// <summary>
    /// Represents a hunk of changes in a diff.
    /// </summary>
    public class Hunk
    {
        /// <summary>
        /// Gets or sets the range of lines in the original file affected by the hunk.
        /// </summary>
        public Range Original { get; set; }

        /// <summary>
        /// Gets or sets the range of lines in the modified file affected by the hunk.
        /// </summary>
        public Range Modified { get; set; }
    }

    /// <summary>
    /// Represents a range of lines in a file.
    /// </summary>
    public class Range
    {
        /// <summary>
        /// Gets or sets the starting line number of the range (1-based).
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// Gets or sets the ending line number of the range.
        /// </summary>
        public int End { get; set; }
    }
}
