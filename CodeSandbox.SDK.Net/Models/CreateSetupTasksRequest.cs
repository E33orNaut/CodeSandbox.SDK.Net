using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents a request to create setup tasks with a list of task definitions.
    /// </summary>
    public class CreateSetupTasksRequest
    {
        /// <summary>
        /// Gets or sets the list of task definitions to be created.
        /// </summary>
        [JsonProperty("tasks")]
        public List<TaskDefinitionDTO> Tasks { get; set; }
    }
}
