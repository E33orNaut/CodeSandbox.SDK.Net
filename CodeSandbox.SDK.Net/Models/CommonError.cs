namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents a common error response returned by the API.
    /// </summary>
    public class CommonError
    {
        /// <summary>
        /// Gets or sets the error code indicating the type of error.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the error message describing the error.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets additional error data, if any.
        /// Use a more specific type if the structure is known.
        /// </summary>
        public object Data { get; set; }
    }
}
