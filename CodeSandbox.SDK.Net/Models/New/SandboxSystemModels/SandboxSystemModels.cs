using System.Collections.Generic;

namespace CodeSandbox.SDK.Net.Models.New.SandboxSystemModels
{
    // --- Success Response ---
    public class SandboxSystemSuccessResponse
    {
        public int Status { get; set; }
        public object Result { get; set; }
    }

    // --- Error Response ---
    public class SandboxSystemErrorResponse
    {
        public int Status { get; set; }
        public SandboxSystemError Error { get; set; }
    }

    // --- System Error ---
    public class SandboxSystemError
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

    // --- System Metrics Status ---
    public class SandboxSystemMetricsStatus
    {
        public SandboxSystemCpuMetrics Cpu { get; set; }
        public SandboxSystemMemoryMetrics Memory { get; set; }
        public SandboxSystemStorageMetrics Storage { get; set; }
    }

    public class SandboxSystemCpuMetrics
    {
        public double Cores { get; set; }
        public double Used { get; set; }
        public double Configured { get; set; }
    }

    public class SandboxSystemMemoryMetrics
    {
        public double Used { get; set; }
        public double Total { get; set; }
        public double Configured { get; set; }
    }

    public class SandboxSystemStorageMetrics
    {
        public double Used { get; set; }
        public double Total { get; set; }
        public double Configured { get; set; }
    }

    // --- Init Status ---
    public class SandboxSystemInitStatus
    {
        public string Message { get; set; }
        public bool? IsError { get; set; }
        public double Progress { get; set; }
        public double NextProgress { get; set; }
        public string Stdout { get; set; }
    }
}