using System.Collections.Generic;

namespace CodeSandbox.SDK.Net.Models.New.PortModels
{
    /// <summary>
    /// Represents a successful response from the Port API.
    /// </summary>
    public class PortSuccessResponse
    {
        public int Status { get; set; }
        public PortListResult Result { get; set; }
    }

    /// <summary>
    /// Represents the result payload for a successful port list operation.
    /// </summary>
    public class PortListResult
    {
        public List<PortModel> List { get; set; }
    }

    /// <summary>
    /// Represents a port and its associated URL.
    /// </summary>
    public class PortModel
    {
        public int Port { get; set; }
        public string Url { get; set; }
    }

    /// <summary>
    /// Represents an error response from the Port API.
    /// </summary>
    public class PortErrorResponse
    {
        public int Status { get; set; }
        public PortCommonError Error { get; set; }
    }

    /// <summary>
    /// Represents common error details for the Port API.
    /// </summary>
    public class PortCommonError
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}