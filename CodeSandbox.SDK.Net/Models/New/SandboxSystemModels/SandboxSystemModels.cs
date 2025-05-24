using System.Collections.Generic;
/// <summary></summary>
namespace CodeSandbox.SDK.Net.Models.New.SandboxSystemModels
{
    // --- Success Response --- 
    public class SandboxSystemSuccessResponse
    {
        public int Status { get; set; } // 0 for success
        public object Result { get; set; }
    }

    // --- Error Response ---
    public class SandboxSystemErrorResponse
    {
        public int Status { get; set; } // 1 for error
        public SandboxSystemError Error { get; set; }
    }

    // --- Error Details ---
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
}