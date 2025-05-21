using System.Collections.Generic;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents the current status of a Git repository including changed files, branch info, and commits.
    /// </summary>
    public class GitStatus
    {
        /// <summary>
        /// Gets or sets the collection of files that have been changed in the working directory.
        /// </summary>
        public GitChangedFiles ChangedFiles { get; set; }

        /// <summary>
        /// Gets or sets the list of files that have been deleted.
        /// </summary>
        public List<GitItem> DeletedFiles { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether there are conflicts in the repository.
        /// </summary>
        public bool Conflicts { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether there are any local changes.
        /// </summary>
        public bool LocalChanges { get; set; }

        /// <summary>
        /// Gets or sets the remote branch properties.
        /// </summary>
        public GitBranchProperties Remote { get; set; }

        /// <summary>
        /// Gets or sets the target branch properties.
        /// </summary>
        public GitBranchProperties Target { get; set; }

        /// <summary>
        /// Gets or sets the current HEAD commit hash.
        /// </summary>
        public string Head { get; set; }

        /// <summary>
        /// Gets or sets the list of recent commits.
        /// </summary>
        public List<GitCommit> Commits { get; set; }

        /// <summary>
        /// Gets or sets the current branch name.
        /// </summary>
        public string Branch { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a merge operation is in progress.
        /// </summary>
        public bool IsMerging { get; set; }
    }
}
