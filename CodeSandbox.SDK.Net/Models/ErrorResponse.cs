using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents an error response from the API.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Gets or sets the HTTP status code of the error response.
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the detailed error information.
        /// </summary>
        [JsonProperty("error")]
        public CommonError Error { get; set; }
    }
}
