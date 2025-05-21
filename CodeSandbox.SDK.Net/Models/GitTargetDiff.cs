using System.Collections.Generic;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents the difference between two Git targets including commits and ahead/behind counts.
    /// </summary>
    public class GitTargetDiff
    {
        /// <summary>
        /// Gets or sets the number of commits the source branch is ahead of the target branch.
        /// </summary>
        public int Ahead { get; set; }

        /// <summary>
        /// Gets or sets the number of commits the source branch is behind the target branch.
        /// </summary>
        public int Behind { get; set; }

        /// <summary>
        /// Gets or sets the list of commits representing the difference.
        /// </summary>
        public List<GitCommit> Commits { get; set; }
    }
}
