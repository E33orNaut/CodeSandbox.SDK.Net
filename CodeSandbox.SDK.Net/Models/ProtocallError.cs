namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents a protocol-level error returned by the API.
    /// </summary>
    public class ProtocolError
    {
        /// <summary>
        /// Gets or sets the error code identifying the error type.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the descriptive error message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets any additional data related to the error.
        /// </summary>
        public object Data { get; set; }
    }
}
