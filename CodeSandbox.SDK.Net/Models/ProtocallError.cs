using Newtonsoft.Json;

    namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents a protocol error returned from the API.
    /// </summary>
    public class ProtocolError
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; }
    }
}
