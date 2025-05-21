namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents the response containing the status and list of ports.
    /// </summary>
    public class PortListResponse
    {
        /// <summary>
        /// Gets or sets the status code of the response.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the result containing the list of ports.
        /// </summary>
        public PortListResult Result { get; set; }
    }
}
