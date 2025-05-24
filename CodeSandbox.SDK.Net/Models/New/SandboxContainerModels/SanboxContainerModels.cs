using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models.New.SandboxContainerModels
{
    /// <summary>
    /// Request model for /container/setup endpoint.
    /// </summary>
    public class ContainerSetupRequest
    {
        /// <summary>
        /// The template ID to use for the container setup.
        /// </summary>
        [JsonProperty("templateId")]
        public string TemplateId { get; set; }

        /// <summary>
        /// Arguments to pass to the template.
        /// </summary>
        [JsonProperty("templateArgs")]
        public Dictionary<string, string> TemplateArgs { get; set; }

        /// <summary>
        /// List of features to enable in the container.
        /// </summary>
        [JsonProperty("features")]
        public List<ContainerSetupFeature> Features { get; set; }
    }

    /// <summary>
    /// Represents a feature in the container setup request.
    /// </summary>
    public class ContainerSetupFeature
    {
        /// <summary>
        /// The feature ID.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Options for the feature.
        /// </summary>
        [JsonProperty("options")]
        public Dictionary<string, string> Options { get; set; }
    }

    /// <summary>
    /// Success response for /container/setup.
    /// </summary>
    public class ContainerSetupSuccessResponse
    {
        /// <summary>
        /// Status code for successful operations.
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        /// Result payload for the operation.
        /// </summary>
        [JsonProperty("result")]
        public ContainerSetupTaskDTO Result { get; set; }
    }

    /// <summary>
    /// Task information returned in a successful container setup.
    /// </summary>
    public class ContainerSetupTaskDTO
    {
        /// <summary>
        /// The task ID.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The status of the task.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// The progress of the task (0-100).
        /// </summary>
        [JsonProperty("progress")]
        public double Progress { get; set; }
    }

    /// <summary>
    /// Error response for /container/setup.
    /// </summary>
    public class ContainerSetupErrorResponse
    {
        /// <summary>
        /// Status code for error operations.
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        /// Error details.
        /// </summary>
        [JsonProperty("error")]
        public ContainerSetupProtocolError Error { get; set; }
    }

    /// <summary>
    /// Protocol error details for container setup.
    /// </summary>
    public class ContainerSetupProtocolError
    {
        /// <summary>
        /// Error code.
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// Error message.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Additional error data, if any.
        /// </summary>
        [JsonProperty("data")]
        public object Data { get; set; }
    }
}