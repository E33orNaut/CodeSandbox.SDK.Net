using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models.New.PortModels
{
    /// <summary>
    /// Represents a successful response from the Port API.
    /// </summary>
    public class PortSuccessResponse
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
        public PortListResult Result { get; set; }
    }

    /// <summary>
    /// Represents the result payload for a successful port list operation.
    /// </summary>
    public class PortListResult
    {
        /// <summary>
        /// List of ports and their associated URLs.
        /// </summary>
        [JsonProperty("list")]
        public List<PortModel> List { get; set; }
    }

    /// <summary>
    /// Represents a port and its associated URL.
    /// </summary>
    public class PortModel
    {
        /// <summary>
        /// The port number.
        /// </summary>
        [JsonProperty("port")]
        public int Port { get; set; }

        /// <summary>
        /// The URL associated with the port.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }
    }

    /// <summary>
    /// Represents an error response from the Port API.
    /// </summary>
    public class PortErrorResponse
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
        public PortCommonError Error { get; set; }
    }

    /// <summary>
    /// Represents common error details for the Port API.
    /// </summary>
    public class PortCommonError
    {
        /// <summary>
        /// Error code.
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }

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