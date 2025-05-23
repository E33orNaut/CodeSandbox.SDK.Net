using System.Collections.Generic;

namespace OpenSandbox.SDK.Net.Models.New.SandboxSetupModels
{
    // --- Success Response ---
    public class SandboxSetupSuccessResponse
    {
        public int Status { get; set; }
        public SandboxSetupProgress Result { get; set; }
    }

    // --- Error Response ---
    public class SandboxSetupErrorResponse
    {
        public int Status { get; set; }
        public SandboxSetupProtocolError Error { get; set; }
    }

    // --- Protocol Error ---
    public class SandboxSetupProtocolError
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

    // --- Setup Progress ---
    public class SandboxSetupProgress
    {
        public SandboxSetupState State { get; set; }
        public List<SandboxSetupStep> Steps { get; set; }
        public int CurrentStepIndex { get; set; }
    }

    public enum SandboxSetupState
    {
        IDLE,
        IN_PROGRESS,
        FINISHED,
        STOPPED
    }

    // --- Step ---
    public class SandboxSetupStep
    {
        public string Name { get; set; }
        public string Command { get; set; }
        public string ShellId { get; set; }
        public SandboxSetupShellStatus? FinishStatus { get; set; }
    }

    public enum SandboxSetupShellStatus
    {
        SUCCEEDED,
        FAILED,
        SKIPPED
    }
}