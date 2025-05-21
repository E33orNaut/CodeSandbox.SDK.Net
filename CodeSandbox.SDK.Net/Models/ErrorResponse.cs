namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents a standardized error response from the API.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Gets or sets the HTTP status code of the error response.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the detailed error information.
        /// </summary>
        public CommonError Error { get; set; }
    }
}
