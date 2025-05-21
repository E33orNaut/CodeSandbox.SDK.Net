namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents the result of a commit operation.
    /// </summary>
    public class CommitResult
    {
        /// <summary>
        /// Gets or sets the status code of the commit operation.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the detailed result data of the commit operation.
        /// </summary>
        public CommitResultData Result { get; set; }
    }

    /// <summary>
    /// Contains detailed data returned after a commit operation.
    /// </summary>
    public class CommitResultData
    {
        /// <summary>
        /// Gets or sets the shell identifier related to the commit.
        /// </summary>
        public string ShellId { get; set; }
    }
}
