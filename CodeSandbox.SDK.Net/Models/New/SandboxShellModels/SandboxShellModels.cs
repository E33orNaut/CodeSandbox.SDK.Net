using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models.New.SandboxShellModels
{
    // --- Common Response Models ---

    /// <summary>
    /// Represents a successful response with a result of type T.
    /// </summary>
    public class SandboxShellSuccessResponse<T>
    {
        /// <summary>
        /// HTTP status code of the response.
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
    /// Represents a successful response with a string result.
    /// </summary>
    public class SandboxShellSuccessResponse
    {
        /// <summary>
        /// HTTP status code of the response.
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        /// The result payload as a string.
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Represents an error response.
    /// </summary>
    public class SandboxShellErrorResponse
    {
        /// <summary>
        /// HTTP status code of the response.
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        /// Error details.
        /// </summary>
        [JsonProperty("error")]
        public SandboxShellCommonError Error { get; set; }
    }

    // --- Common Error Model ---

    /// <summary>
    /// Represents a common error structure.
    /// </summary>
    public class SandboxShellCommonError
    {
        /// <summary>
        /// Error code identifier.
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

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

    // --- ShellId ---

    /// <summary>
    /// Represents a shell identifier.
    /// </summary>
    public class SandboxShellId
    {
        /// <summary>
        /// The shell's unique identifier.
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    // --- ShellSize ---

    /// <summary>
    /// Represents the size of a shell (columns and rows).
    /// </summary>
    public class SandboxShellSize
    {
        /// <summary>
        /// Number of columns.
        /// </summary>
        [JsonProperty("cols")]
        public int Cols { get; set; }

        /// <summary>
        /// Number of rows.
        /// </summary>
        [JsonProperty("rows")]
        public int Rows { get; set; }
    }

    // --- ShellProcessType ---

    /// <summary>
    /// Enumerates the types of shell processes.
    /// </summary>
    public enum SandboxShellProcessType
    {
        TERMINAL,
        COMMAND
    }

    // --- ShellProcessStatus ---

    /// <summary>
    /// Enumerates the statuses of a shell process.
    /// </summary>
    public enum SandboxShellProcessStatus
    {

        RUNNING,
        FINISHED,
        ERROR,
        KILLED,
        RESTARTING
    }

    // --- BaseShellDTO ---

    /// <summary>
    /// Base class for shell data transfer objects.
    /// </summary>
    public class SandboxBaseShellDTO
    {
        /// <summary>
        /// The shell's unique identifier.
        /// </summary>
        [JsonProperty("shellId")]
        public string ShellId { get; set; }

        /// <summary>
        /// The shell's display name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The current status of the shell process.
        /// </summary>
        [JsonProperty("status")]
        public SandboxShellProcessStatus Status { get; set; }

        /// <summary>
        /// The exit code if the process has finished.
        /// </summary>
        [JsonProperty("exitCode")]
        public int? ExitCode { get; set; }
    }

    // --- CommandShellDTO ---

    /// <summary>
    /// Represents a command shell DTO.
    /// </summary>
    public class SandboxCommandShellDTO : SandboxBaseShellDTO
    {
        /// <summary>
        /// The type of shell ("COMMAND").
        /// </summary>
        [JsonProperty("shellType")]
        public string ShellType { get; set; }

        /// <summary>
        /// The command used to start the shell.
        /// </summary>
        [JsonProperty("startCommand")]
        public string StartCommand { get; set; }
    }

    // --- TerminalShellDTO ---

    /// <summary>
    /// Represents a terminal shell DTO.
    /// </summary>
    public class SandboxTerminalShellDTO : SandboxBaseShellDTO
    {
        /// <summary>
        /// The type of shell ("TERMINAL").
        /// </summary>
        [JsonProperty("shellType")]
        public string ShellType { get; set; }

        /// <summary>
        /// The username of the shell owner.
        /// </summary>
        [JsonProperty("ownerUsername")]
        public string OwnerUsername { get; set; }

        /// <summary>
        /// Indicates if this is a system shell.
        /// </summary>
        [JsonProperty("isSystemShell")]
        public bool IsSystemShell { get; set; }
    }

    // --- ShellDTO (Union) ---

    /// <summary>
    /// Represents a union of command and terminal shell DTOs.
    /// </summary>
    public class SandboxShellDTO
    {
        /// <summary>
        /// The type of shell.
        /// </summary>
        [JsonProperty("shellType")]
        public string ShellType { get; set; }

        /// <summary>
        /// The command shell DTO, if applicable.
        /// </summary>
        [JsonProperty("commandShell")]
        public SandboxCommandShellDTO CommandShell { get; set; }

        /// <summary>
        /// The terminal shell DTO, if applicable.
        /// </summary>
        [JsonProperty("terminalShell")]
        public SandboxTerminalShellDTO TerminalShell { get; set; }
    }

    // --- OpenCommandShellDTO ---

    /// <summary>
    /// Represents an open command shell DTO with buffer.
    /// </summary>
    public class SandboxOpenCommandShellDTO : SandboxCommandShellDTO
    {
        /// <summary>
        /// The output buffer.
        /// </summary>
        [JsonProperty("buffer")]
        public List<string> Buffer { get; set; }
    }

    // --- OpenTerminalShellDTO ---

    /// <summary>
    /// Represents an open terminal shell DTO with buffer.
    /// </summary>
    public class SandboxOpenTerminalShellDTO : SandboxTerminalShellDTO
    {
        /// <summary>
        /// The output buffer.
        /// </summary>
        [JsonProperty("buffer")]
        public List<string> Buffer { get; set; }
    }

    // --- OpenShellDTO (Union) ---

    /// <summary>
    /// Represents a union of open command and terminal shell DTOs.
    /// </summary>
    public class SandboxOpenShellDTO
    {
        /// <summary>
        /// The type of shell.
        /// </summary>
        [JsonProperty("shellType")]
        public string ShellType { get; set; }

        /// <summary>
        /// The open command shell DTO, if applicable.
        /// </summary>
        [JsonProperty("commandShell")]
        public SandboxOpenCommandShellDTO CommandShell { get; set; }

        /// <summary>
        /// The open terminal shell DTO, if applicable.
        /// </summary>
        [JsonProperty("terminalShell")]
        public SandboxOpenTerminalShellDTO TerminalShell { get; set; }
    }

    // --- /shell/create request ---

    /// <summary>
    /// Request to create a new shell.
    /// </summary>
    public class SandboxShellCreateRequest
    {
        /// <summary>
        /// The command to execute.
        /// </summary>
        [JsonProperty("command")]
        public string Command { get; set; }

        /// <summary>
        /// The working directory.
        /// </summary>
        [JsonProperty("cwd")]
        public string Cwd { get; set; }

        /// <summary>
        /// The shell size.
        /// </summary>
        [JsonProperty("size")]
        public SandboxShellSize Size { get; set; }

        /// <summary>
        /// The type of shell process.
        /// </summary>
        [JsonProperty("type")]
        public SandboxShellProcessType? Type { get; set; }

        /// <summary>
        /// Indicates if this is a system shell.
        /// </summary>
        [JsonProperty("isSystemShell")]
        public bool? IsSystemShell { get; set; }
    }

    // --- /shell/in request ---

    /// <summary>
    /// Request to send input to a shell.
    /// </summary>
    public class SandboxShellInRequest
    {
        /// <summary>
        /// The shell's unique identifier.
        /// </summary>
        [JsonProperty("shellId")]
        public string ShellId { get; set; }

        /// <summary>
        /// The input to send.
        /// </summary>
        [JsonProperty("input")]
        public string Input { get; set; }

        /// <summary>
        /// The shell size.
        /// </summary>
        [JsonProperty("size")]
        public SandboxShellSize Size { get; set; }
    }

    // --- /shell/open request ---

    /// <summary>
    /// Request to open a shell.
    /// </summary>
    public class SandboxShellOpenRequest
    {
        /// <summary>
        /// The shell's unique identifier.
        /// </summary>
        [JsonProperty("shellId")]
        public string ShellId { get; set; }

        /// <summary>
        /// The shell size.
        /// </summary>
        [JsonProperty("size")]
        public SandboxShellSize Size { get; set; }
    }

    // --- /shell/close, /shell/restart, /shell/terminate, /shell/resize, /shell/rename requests ---

    /// <summary>
    /// Request containing only a shell identifier.
    /// </summary>
    public class SandboxShellIdRequest
    {
        /// <summary>
        /// The shell's unique identifier.
        /// </summary>
        [JsonProperty("shellId")]
        public string ShellId { get; set; }
    }

    /// <summary>
    /// Request to resize a shell.
    /// </summary>
    public class SandboxShellResizeRequest
    {
        /// <summary>
        /// The shell's unique identifier.
        /// </summary>
        [JsonProperty("shellId")]
        public string ShellId { get; set; }

        /// <summary>
        /// The new shell size.
        /// </summary>
        [JsonProperty("size")]
        public SandboxShellSize Size { get; set; }
    }

    /// <summary>
    /// Request to rename a shell.
    /// </summary>
    public class SandboxShellRenameRequest
    {
        /// <summary>
        /// The shell's unique identifier.
        /// </summary>
        [JsonProperty("shellId")]
        public string ShellId { get; set; }

        /// <summary>
        /// The new name for the shell.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    // --- /shell/list response ---

    /// <summary>
    /// Result of a shell list operation.
    /// </summary>
    public class SandboxShellListResult
    {
        /// <summary>
        /// The list of shells.
        /// </summary>
        [JsonProperty("shells")]
        public List<SandboxShellDTO> Shells { get; set; }
    }

    // --- /shell/terminate response ---
    // Returns a ShellDTO

    // --- /shell/open and /shell/create response ---
    // Returns an OpenShellDTO
}