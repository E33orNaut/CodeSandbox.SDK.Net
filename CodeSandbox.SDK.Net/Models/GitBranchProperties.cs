namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents properties and status of a Git branch.
    /// </summary>
    public class GitBranchProperties
    {
        /// <summary>
        /// Gets or sets the commit hash of the branch head.
        /// </summary>
        public string Head { get; set; }

        /// <summary>
        /// Gets or sets the name of the branch.
        /// </summary>
        public string Branch { get; set; }

        /// <summary>
        /// Gets or sets the number of commits the branch is ahead of the remote branch.
        /// </summary>
        public int Ahead { get; set; }

        /// <summary>
        /// Gets or sets the number of commits the branch is behind the remote branch.
        /// </summary>
        public int Behind { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the branch is considered safe (e.g., no conflicts or issues).
        /// </summary>
        public bool Safe { get; set; }
    }
}
