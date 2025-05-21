using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models
{
    public class SystemError
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; }
    }

}
