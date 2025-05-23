using System.Collections.Generic;

namespace CodeSandbox.SDK.Net.Models.New.SandboxTaskModels
{
    // --- Success Response ---
    public class SandboxTaskSuccessResponse<T>
    {
        public int Status { get; set; }
        public T Result { get; set; }
    }

    // --- Error Response ---
    public class SandboxTaskErrorResponse
    {
        public int Status { get; set; }
        public SandboxTaskErrorUnion Error { get; set; }
    }

    // --- Common Error ---
    public class SandboxTaskCommonError
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

    // --- Task Error (Discriminated Union) ---
    public class SandboxTaskErrorUnion
    {
        public SandboxTaskCommonError CommonError { get; set; }
        public SandboxTaskConfigFileAlreadyExistsError ConfigFileAlreadyExists { get; set; }
        public SandboxTaskNotFoundError TaskNotFound { get; set; }
        public SandboxTaskCommandAlreadyConfiguredError CommandAlreadyConfigured { get; set; }
    }

    public class SandboxTaskConfigFileAlreadyExistsError
    {
        public int Code { get; set; } // 600
        public string Message { get; set; }
    }

    public class SandboxTaskNotFoundError
    {
        public int Code { get; set; } // 601
        public string Message { get; set; }
    }

    public class SandboxTaskCommandAlreadyConfiguredError
    {
        public int Code { get; set; } // 602
        public string Message { get; set; }
    }

    // --- Task Definition DTO ---
    public class SandboxTaskDefinitionDTO
    {
        public string Name { get; set; }
        public string Command { get; set; }
        public bool? RunAtStart { get; set; }
        public SandboxTaskPreview Preview { get; set; }
    }

    public class SandboxTaskPreview
    {
        public double? Port { get; set; }
        public SandboxTaskPrLinkType? PrLink { get; set; }
    }

    public enum SandboxTaskPrLinkType
    {
        direct,
        redirect,
        devtool
    }

    // --- Command Shell DTO ---
    public class SandboxCommandShellDTO
    {
        public string Id { get; set; }
        public string Command { get; set; }
        public SandboxCommandShellStatus Status { get; set; }
        public string Output { get; set; }
    }

    public enum SandboxCommandShellStatus
    {
        initializing,
        running,
        stopped,
        error
    }

    // --- Port ---
    public class SandboxTaskPort
    {
        public double Port { get; set; }
        public string Hostname { get; set; }
        public SandboxTaskPortStatus Status { get; set; }
        public string TaskId { get; set; }
    }

    public enum SandboxTaskPortStatus
    {
        open,
        closed
    }

    // --- Task DTO ---
    public class SandboxTaskDTO
    {
        public string Name { get; set; }
        public string Command { get; set; }
        public bool? RunAtStart { get; set; }
        public SandboxTaskPreview Preview { get; set; }
        public string Id { get; set; }
        public bool? Unconfigured { get; set; }
        public SandboxCommandShellDTO Shell { get; set; }
        public List<SandboxTaskPort> Ports { get; set; }
    }

    // --- Task List DTO ---
    public class SandboxTaskListDTO
    {
        public Dictionary<string, SandboxTaskDTO> Tasks { get; set; }
        public List<SandboxTaskDefinitionDTO> SetupTasks { get; set; }
        public List<string> ValidationErrors { get; set; }
    }
}