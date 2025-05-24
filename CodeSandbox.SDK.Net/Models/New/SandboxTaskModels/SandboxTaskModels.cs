using System.Collections.Generic;

namespace CodeSandbox.SDK.Net.Models.New.SandboxTaskModels
{
    // --- Success Response ---
    public class SandboxTaskSuccessResponse<T>
    {
        public int Status { get; set; } // Always 0 for success
        public T Result { get; set; }
    }

    // --- Error Response ---
    public class SandboxTaskErrorResponse
    {
        public int Status { get; set; } // Always 1 for error
        public SandboxTaskError Error { get; set; }
    }

    // --- Task Error (discriminated union) ---
    public class SandboxTaskError
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; } // Optional, for CommonError
    }

    // --- Task List DTO ---
    public class SandboxTaskListResult
    {
        public Dictionary<string, SandboxTaskDTO> Tasks { get; set; }
        public List<SandboxTaskDefinitionDTO> SetupTasks { get; set; }
        public List<string> ValidationErrors { get; set; }
    }

    // --- Task DTO ---
    public class SandboxTaskDTO : SandboxTaskDefinitionDTO
    {
        public string Id { get; set; }
        public bool? Unconfigured { get; set; }
        public SandboxCommandShellDTO Shell { get; set; }
        public List<SandboxPortDTO> Ports { get; set; }
    }

    // --- Task Definition DTO ---
    public class SandboxTaskDefinitionDTO
    {
        public string Name { get; set; }
        public string Command { get; set; }
        public bool? RunAtStart { get; set; }
        public SandboxTaskPreviewDTO Preview { get; set; }
    }

    // --- Task Preview DTO ---
    public class SandboxTaskPreviewDTO
    {
        public double? Port { get; set; }
        public string PrLink { get; set; } // "direct", "redirect", "devtool"
    }

    // --- Command Shell DTO ---
    public class SandboxCommandShellDTO
    {
        public string Id { get; set; }
        public string Command { get; set; }
        public string Status { get; set; } // "initializing", "running", "stopped", "error"
        public string Output { get; set; }
    }

    // --- Port DTO ---
    public class SandboxPortDTO
    {
        public double Port { get; set; }
        public string Hostname { get; set; }
        public string Status { get; set; } // "open", "closed"
        public string TaskId { get; set; }
    }

    // --- Task Result (alias for TaskDTO) ---
    public class SandboxTaskResult : SandboxTaskDTO { }

    // --- Setup Tasks Result (for createSetupTasks, result is null) ---
    public class SandboxTaskSetupTasksResult
    {
        // No properties, result is always null
    }

    // --- Create Setup Tasks Request ---
    public class SandboxTaskCreateSetupTasksRequest
    {
        public List<SandboxTaskDefinitionDTO> Tasks { get; set; }
    }

    // --- Run Command Request ---
    public class SandboxTaskRunCommandRequest
    {
        public string Command { get; set; }
        public string Name { get; set; }
        public bool? SaveToConfig { get; set; }
    }
}