using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CodeSandbox.SDK.Net.Models
{
    public class CreateSetupTasksRequest
    {
        [JsonPropertyName("tasks")]
        public List<TaskDefinitionDTO> Tasks { get; set; }
    }

}
