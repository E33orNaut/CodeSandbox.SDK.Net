using System.Collections.Generic;

namespace CodeSandbox.SDK.New.Models.New.SandboxContainerModels
{
    /// <summary>
    /// Request model for /container/setup endpoint.
    /// </summary>
    public class ContainerSetupRequest
    {
        public string TemplateId { get; set; }
        public Dictionary<string, string> TemplateArgs { get; set; }
        public List<ContainerSetupFeature> Features { get; set; }
    }

    /// <summary>
    /// Represents a feature in the container setup request.
    /// </summary>
    public class ContainerSetupFeature
    {
        public string Id { get; set; }
        public Dictionary<string, string> Options { get; set; }
    }

    /// <summary>
    /// Success response for /container/setup.
    /// </summary>
    public class ContainerSetupSuccessResponse
    {
        public int Status { get; set; }
        public ContainerSetupTaskDTO Result { get; set; }
    }

    /// <summary>
    /// Task information returned in a successful container setup.
    /// </summary>
    public class ContainerSetupTaskDTO
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public double Progress { get; set; }
    }

    /// <summary>
    /// Error response for /container/setup.
    /// </summary>
    public class ContainerSetupErrorResponse
    {
        public int Status { get; set; }
        public ContainerSetupProtocolError Error { get; set; }
    }

    /// <summary>
    /// Protocol error details for container setup.
    /// </summary>
    public class ContainerSetupProtocolError
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}