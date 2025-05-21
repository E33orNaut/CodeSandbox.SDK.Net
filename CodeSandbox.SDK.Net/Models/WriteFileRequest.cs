namespace CodeSandbox.SDK.Models
{
    /// <summary>
    /// Represents a request to write content to a file at a specified path.
    /// </summary>
    public class WriteFileRequest
    {
        /// <summary>
        /// Gets or sets the file path where the content should be written.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the content to write to the file.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the content is binary.
        /// If true, the content is treated as binary data.
        /// </summary>
        public bool? IsBinary { get; set; }
    }
}
