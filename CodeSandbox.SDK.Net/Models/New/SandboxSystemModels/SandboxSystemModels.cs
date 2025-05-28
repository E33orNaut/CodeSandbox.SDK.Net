using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models.New.SandboxSystemModels
{
    /// <summary>
    /// Represents a successful system response.
    /// </summary>
    public class SandboxSystemSuccessResponse
    {
        /// <summary>
        /// Status code (0 for success).
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        /// The result payload.
        /// </summary>
        [JsonProperty("result")]
        public object Result { get; set; }
    }

    /// <summary>
    /// Represents an error system response.
    /// </summary>
    public class SandboxSystemErrorResponse
    {
        /// <summary>
        /// Status code (1 for error).
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        /// Error details.
        /// </summary>
        [JsonProperty("error")]
        public SandboxSystemError Error { get; set; }
    }

    /// <summary>
    /// Contains details about a system error.
    /// </summary>
    public class SandboxSystemError
    {
        /// <summary>
        /// Numeric error code.
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }

        /// <summary>
        /// Human-readable error message.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Additional error data.
        /// </summary>
        [JsonProperty("data")]
        public object Data { get; set; }
    }

    /// <summary>
    /// Represents the status of system metrics.
    /// </summary>
    public class SandboxSystemMetricsStatus
    {
        /// <summary>
        /// CPU metrics.
        /// </summary>
        [JsonProperty("cpu")]
        public SandboxSystemCpuMetrics Cpu { get; set; }

        /// <summary>
        /// Memory metrics.
        /// </summary>
        [JsonProperty("memory")]
        public SandboxSystemMemoryMetrics Memory { get; set; }

        /// <summary>
        /// Storage metrics.
        /// </summary>
        [JsonProperty("storage")]
        public SandboxSystemStorageMetrics Storage { get; set; }
    }

    /// <summary>
    /// Represents CPU metrics.
    /// </summary>
    public class SandboxSystemCpuMetrics
    {
        /// <summary>
        /// Number of CPU cores.
        /// </summary>
        [JsonProperty("cores")]
        public double Cores { get; set; }

        /// <summary>
        /// Amount of CPU used.
        /// </summary>
        [JsonProperty("used")]
        public double Used { get; set; }

        /// <summary>
        /// Amount of CPU configured.
        /// </summary>
        [JsonProperty("configured")]
        public double Configured { get; set; }
    }

    /// <summary>
    /// Represents memory metrics.
    /// </summary>
    public class SandboxSystemMemoryMetrics
    {
        /// <summary>
        /// Amount of memory used.
        /// </summary>
        [JsonProperty("used")]
        public double Used { get; set; }

        /// <summary>
        /// Total memory available.
        /// </summary>
        [JsonProperty("total")]
        public double Total { get; set; }

        /// <summary>
        /// Amount of memory configured.
        /// </summary>
        [JsonProperty("configured")]
        public double Configured { get; set; }
    }

    /// <summary>
    /// Represents storage metrics.
    /// </summary>
    public class SandboxSystemStorageMetrics
    {
        /// <summary>
        /// Amount of storage used.
        /// </summary>
        [JsonProperty("used")]
        public double Used { get; set; }

        /// <summary>
        /// Total storage available.
        /// </summary>
        [JsonProperty("total")]
        public double Total { get; set; }

        /// <summary>
        /// Amount of storage configured.
        /// </summary>
        [JsonProperty("configured")]
        public double Configured { get; set; }
    }
}