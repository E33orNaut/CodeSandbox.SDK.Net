namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents the response containing remote content data.
    /// </summary>
    public class RemoteContentResult
    {
        /// <summary>
        /// Gets or sets the status code of the response.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the result data containing the remote content.
        /// </summary>
        public RemoteContentData Result { get; set; }
    }

    /// <summary>
    /// Represents the remote content data.
    /// </summary>
    public class RemoteContentData
    {
        /// <summary>
        /// Gets or sets the content retrieved remotely as a string.
        /// </summary>
        public string Content { get; set; }
    }
}
