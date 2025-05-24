using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CodeSandbox.SDK.Net.Models.New.SandboxSetupModels
{
    // --- Success Response ---

    /// <summary>
    /// Represents a successful response from the sandbox setup API.
    /// </summary>
    public class SandboxSetupSuccessResponse
    {
        /// <summary>
        /// Status code for successful operations.
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        /// Result payload for the operation, containing setup progress.
        /// </summary>
        [JsonProperty("result")]
        public SandboxSetupProgress Result { get; set; }
    }

    // --- Error Response ---

    /// <summary>
    /// Represents an error response from the sandbox setup API.
    /// </summary>
    public class SandboxSetupErrorResponse
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
        public SandboxSetupProtocolError Error { get; set; }
    }

    // --- Protocol Error ---

    /// <summary>
    /// Protocol error details for sandbox setup.
    /// </summary>
    public class SandboxSetupProtocolError
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

    // --- Setup Progress ---

    /// <summary>
    /// Represents the progress of a sandbox setup operation.
    /// </summary>
    public class SandboxSetupProgress
    {
        /// <summary>
        /// The current state of the setup.
        /// </summary>
        [JsonProperty("state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SandboxSetupState State { get; set; }

        /// <summary>
        /// The list of steps in the setup process.
        /// </summary>
        [JsonProperty("steps")]
        public List<SandboxSetupStep> Steps { get; set; }

        /// <summary>
        /// The index of the current step.
        /// </summary>
        [JsonProperty("currentStepIndex")]
        public int CurrentStepIndex { get; set; }
    }

    /// <summary>
    /// The state of the sandbox setup process.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SandboxSetupState
    {
        IDLE,
        IN_PROGRESS,
        FINISHED,
        STOPPED
    }

    // --- Step ---

    /// <summary>
    /// Represents a single step in the sandbox setup process.
    /// </summary>
    public class SandboxSetupStep
    {
        /// <summary>
        /// The name of the step.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The command executed in this step.
        /// </summary>
        [JsonProperty("command")]
        public string Command { get; set; }

        /// <summary>
        /// The shell ID associated with this step.
        /// </summary>
        [JsonProperty("shellId")]
        public string ShellId { get; set; }

        /// <summary>
        /// The finish status of the shell command.
        /// </summary>
        [JsonProperty("finishStatus")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SandboxSetupShellStatus? FinishStatus { get; set; }
    }

    /// <summary>
    /// The finish status of a shell command in the setup process.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SandboxSetupShellStatus
    {
        SUCCEEDED,
        FAILED,
        SKIPPED
    }
}