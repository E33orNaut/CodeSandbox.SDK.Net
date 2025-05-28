using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models.New.SandboxTaskModels
{
    /// <summary>
    /// Represents a successful response for a task operation.
    /// </summary>
    public class SandboxTaskSuccessResponse<T>
    {
        /// <summary>
        /// Status code (always 0 for success).
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        /// The result payload.
        /// </summary>
        [JsonProperty("result")]
        public T Result { get; set; }
    }

    /// <summary>
    /// Represents an error response for a task operation.
    /// </summary>
    public class SandboxTaskErrorResponse
    {
        /// <summary>
        /// Status code (always 1 for error).
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        /// Error details.
        /// </summary>
        [JsonProperty("error")]
        public SandboxTaskError Error { get; set; }
    }

    /// <summary>
    /// Represents a task error (discriminated union).
    /// </summary>
    public class SandboxTaskError
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
        /// Additional error data (optional).
        /// </summary>
        [JsonProperty("data")]
        public object Data { get; set; }
    }

    /// <summary>
    /// Represents the result of a task list operation.
    /// </summary>
    public class SandboxTaskListResult
    {
        /// <summary>
        /// Dictionary of tasks by ID.
        /// </summary>
        [JsonProperty("tasks")]
        public Dictionary<string, SandboxTaskDTO> Tasks { get; set; }

        /// <summary>
        /// List of setup tasks.
        /// </summary>
        [JsonProperty("setupTasks")]
        public List<SandboxTaskDefinitionDTO> SetupTasks { get; set; }

        /// <summary>
        /// List of validation errors.
        /// </summary>
        [JsonProperty("validationErrors")]
        public List<string> ValidationErrors { get; set; }
    }

    /// <summary>
    /// Represents a task DTO.
    /// </summary>
    public class SandboxTaskDTO : SandboxTaskDefinitionDTO
    {
        /// <summary>
        /// The unique task identifier.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Indicates if the task is unconfigured.
        /// </summary>
        [JsonProperty("unconfigured")]
        public bool? Unconfigured { get; set; }

        /// <summary>
        /// The shell associated with the task.
        /// </summary>
        [JsonProperty("shell")]
        public SandboxCommandShellDTO Shell { get; set; }

        /// <summary>
        /// List of ports associated with the task.
        /// </summary>
        [JsonProperty("ports")]
        public List<SandboxPortDTO> Ports { get; set; }
    }

    /// <summary>
    /// Represents a task definition DTO.
    /// </summary>
    public class SandboxTaskDefinitionDTO
    {
        /// <summary>
        /// The task name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The command to execute.
        /// </summary>
        [JsonProperty("command")]
        public string Command { get; set; }

        /// <summary>
        /// Indicates if the task should run at start.
        /// </summary>
        [JsonProperty("runAtStart")]
        public bool? RunAtStart { get; set; }

        /// <summary>
        /// Preview information for the task.
        /// </summary>
        [JsonProperty("preview")]
        public SandboxTaskPreviewDTO Preview { get; set; }
    }

    /// <summary>
    /// Represents a preview for a task.
    /// </summary>
    public class SandboxTaskPreviewDTO
    {
        /// <summary>
        /// The port to preview (if any).
        /// </summary>
        [JsonProperty("port")]
        public double? Port { get; set; }

        /// <summary>
        /// The PR link type ("direct", "redirect", "devtool").
        /// </summary>
        [JsonProperty("prLink")]
        public string PrLink { get; set; }
    }

    /// <summary>
    /// Represents a command shell DTO.
    /// </summary>
    public class SandboxCommandShellDTO
    {
        /// <summary>
        /// The shell's unique identifier.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The command executed in the shell.
        /// </summary>
        [JsonProperty("command")]
        public string Command { get; set; }

        /// <summary>
        /// The shell's status ("initializing", "running", "stopped", "error").
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// The output of the shell.
        /// </summary>
        [JsonProperty("output")]
        public string Output { get; set; }
    }

    /// <summary>
    /// Represents a port DTO.
    /// </summary>
    public class SandboxPortDTO
    {
        /// <summary>
        /// The port number.
        /// </summary>
        [JsonProperty("port")]
        public double Port { get; set; }

        /// <summary>
        /// The hostname associated with the port.
        /// </summary>
        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        /// <summary>
        /// The port status ("open", "closed").
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// The ID of the task associated with the port.
        /// </summary>
        [JsonProperty("taskId")]
        public string TaskId { get; set; }
    }

    /// <summary>
    /// Alias for SandboxTaskDTO.
    /// </summary>
    public class SandboxTaskResult : SandboxTaskDTO { }

    /// <summary>
    /// Represents the result for setup tasks (always null).
    /// </summary>
    public class SandboxTaskSetupTasksResult
    {
        // No properties, result is always null
    }

    /// <summary>
    /// Request to create setup tasks.
    /// </summary>
    public class SandboxTaskCreateSetupTasksRequest
    {
        /// <summary>
        /// List of task definitions to create.
        /// </summary>
        [JsonProperty("tasks")]
        public List<SandboxTaskDefinitionDTO> Tasks { get; set; }
    }

    /// <summary>
    /// Request to run a command as a task.
    /// </summary>
    public class SandboxTaskRunCommandRequest
    {
        /// <summary>
        /// The command to run.
        /// </summary>
        [JsonProperty("command")]
        public string Command { get; set; }

        /// <summary>
        /// The name of the task.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Indicates if the command should be saved to config.
        /// </summary>
        [JsonProperty("saveToConfig")]
        public bool? SaveToConfig { get; set; }
    }

    /// <summary>
    /// Request model for starting a sandbox task.
    /// </summary>
    public class SandboxTaskStartRequest
    {
        /// <summary>
        /// The unique identifier of the task to start.
        /// </summary>
        public string TaskId { get; set; }
    }
  

}