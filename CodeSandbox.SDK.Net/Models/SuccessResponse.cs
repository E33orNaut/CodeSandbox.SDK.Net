using Newtonsoft.Json;

namespace CodeSandbox.SDK.Models
{
    /// <summary>
    /// Represents a generic success response from the API.
    /// </summary>
    public class SuccessResponse
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("result")]
        public object Result { get; set; }
    }
}
