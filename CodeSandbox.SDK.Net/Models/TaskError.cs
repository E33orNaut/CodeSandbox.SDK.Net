using System.Text.Json.Serialization;

namespace CodeSandbox.SDK.Net.Models
{
    public class TaskError
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
