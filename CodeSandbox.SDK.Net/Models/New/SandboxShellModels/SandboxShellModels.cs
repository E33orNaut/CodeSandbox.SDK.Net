using System.Collections.Generic;

namespace CodeSandbox.SDK.Net.Models.New.SandboxShellModels
{
    // --- Common Response Models ---

    public class SandboxShellSuccessResponse<T>
    {
        public int Status { get; set; }
        public T Result { get; set; }
    }
    public class SandboxShellSuccessResponse 
    {
        public int Status { get; set; }
        public string Result { get; set; }
    }
    public class SandboxShellErrorResponse
    {
        public int Status { get; set; }
        public SandboxShellCommonError Error { get; set; }
    }

    // --- Common Error Model ---

    public class SandboxShellCommonError
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

    // --- ShellId ---
    public class SandboxShellId
    {
        public string Value { get; set; }
    }

    // --- ShellSize ---
    public class SandboxShellSize
    {
        public int Cols { get; set; }
        public int Rows { get; set; }
    }

    // --- ShellProcessType ---
    public enum SandboxShellProcessType
    {
        TERMINAL,
        COMMAND
    }

    // --- ShellProcessStatus ---
    public enum SandboxShellProcessStatus
    {
        RUNNING,
        FINISHED,
        ERROR,
        KILLED,
        RESTARTING
    }

    // --- BaseShellDTO ---
    public class SandboxBaseShellDTO
    {
        public string ShellId { get; set; }
        public string Name { get; set; }
        public SandboxShellProcessStatus Status { get; set; }
        public int? ExitCode { get; set; }
    }

    // --- CommandShellDTO ---
    public class SandboxCommandShellDTO : SandboxBaseShellDTO
    {
        public string ShellType { get; set; } // "COMMAND"
        public string StartCommand { get; set; }
    }

    // --- TerminalShellDTO ---
    public class SandboxTerminalShellDTO : SandboxBaseShellDTO
    {
        public string ShellType { get; set; } // "TERMINAL"
        public string OwnerUsername { get; set; }
        public bool IsSystemShell { get; set; }
    }

    // --- ShellDTO (Union) ---
    public class SandboxShellDTO
    {
        // Use ShellType to determine which properties are populated
        public string ShellType { get; set; }
        public SandboxCommandShellDTO CommandShell { get; set; }
        public SandboxTerminalShellDTO TerminalShell { get; set; }
    }

    // --- OpenCommandShellDTO ---
    public class SandboxOpenCommandShellDTO : SandboxCommandShellDTO
    {
        public List<string> Buffer { get; set; }
    }

    // --- OpenTerminalShellDTO ---
    public class SandboxOpenTerminalShellDTO : SandboxTerminalShellDTO
    {
        public List<string> Buffer { get; set; }
    }

    // --- OpenShellDTO (Union) ---
    public class SandboxOpenShellDTO
    {
        public string ShellType { get; set; }
        public SandboxOpenCommandShellDTO CommandShell { get; set; }
        public SandboxOpenTerminalShellDTO TerminalShell { get; set; }
    }

    // --- /shell/create request ---
    public class SandboxShellCreateRequest
    {
        public string Command { get; set; }
        public string Cwd { get; set; }
        public SandboxShellSize Size { get; set; }
        public SandboxShellProcessType? Type { get; set; }
        public bool? IsSystemShell { get; set; }
    }

    // --- /shell/in request ---
    public class SandboxShellInRequest
    {
        public string ShellId { get; set; }
        public string Input { get; set; }
        public SandboxShellSize Size { get; set; }
    }

    // --- /shell/open request ---
    public class SandboxShellOpenRequest
    {
        public string ShellId { get; set; }
        public SandboxShellSize Size { get; set; }
    }

    // --- /shell/close, /shell/restart, /shell/terminate, /shell/resize, /shell/rename requests ---
    public class SandboxShellIdRequest
    {
        public string ShellId { get; set; }
    }

    public class SandboxShellResizeRequest
    {
        public string ShellId { get; set; }
        public SandboxShellSize Size { get; set; }
    }

    public class SandboxShellRenameRequest
    {
        public string ShellId { get; set; }
        public string Name { get; set; }
    }

    // --- /shell/list response ---
    public class SandboxShellListResult
    {
        public List<SandboxShellDTO> Shells { get; set; }
    }

    // --- /shell/terminate response ---
    // Returns a ShellDTO

    // --- /shell/open and /shell/create response ---
    // Returns an OpenShellDTO
}