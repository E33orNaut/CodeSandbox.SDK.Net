namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents the response containing the Git status information.
    /// </summary>
    public class GitStatusResult
    {
        /// <summary>
        /// Gets or sets the status code of the response.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the Git status result data.
        /// </summary>
        public GitStatus Result { get; set; }
    }

    /// <summary>
    /// Represents the response containing Git remote repository information.
    /// </summary>
    public class GitRemotesResult
    {
        /// <summary>
        /// Gets or sets the status code of the response.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the Git remotes information.
        /// </summary>
        public GitRemotes Result { get; set; }
    }

    /// <summary>
    /// Represents the response containing Git target diff information.
    /// </summary>
    public class GitTargetDiffResult
    {
        /// <summary>
        /// Gets or sets the status code of the response.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the Git target diff result data.
        /// </summary>
        public GitTargetDiff Result { get; set; }
    }

    /// <summary>
    /// Represents an error response with status and error details.
    /// </summary>
    public class ErrorResult
    {
        /// <summary>
        /// Gets or sets the status code of the response.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the error details.
        /// </summary>
        public CommonError Error { get; set; }
    }
}
