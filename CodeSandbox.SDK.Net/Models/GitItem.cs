namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents a Git item (file) and its status in the index and working tree.
    /// </summary>
    public class GitItem
    {
        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the index status of the file.
        /// Possible values include: "", "M" (modified), "A" (added), "D" (deleted), etc.
        /// </summary>
        public string Index { get; set; }

        /// <summary>
        /// Gets or sets the working tree status of the file.
        /// </summary>
        public string WorkingTree { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the file is staged for commit.
        /// </summary>
        public bool IsStaged { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the file has conflicts.
        /// </summary>
        public bool IsConflicted { get; set; }

        /// <summary>
        /// Gets or sets the unique file identifier.
        /// </summary>
        public string FileId { get; set; }
    }
}
