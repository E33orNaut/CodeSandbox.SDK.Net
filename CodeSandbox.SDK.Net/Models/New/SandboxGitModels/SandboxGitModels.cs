using System.Collections.Generic;

namespace CodeSandbox.SDK.Models.New.SandboxGitModels
{
    // --- Common Response Models ---

    public class SandboxGitSuccessResponse
    {
        public int Status { get; set; }
        public object Result { get; set; }
    } 

    public class SandboxGitErrorResponse
    {
        public int Status { get; set; }
        public SandboxGitCommonError Error { get; set; }
    }

    // --- Common Error Models ---

    public class SandboxGitCommonError 
    {
        // This class can represent either a protocol error or a git error
        public string Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; } // Only present for protocol errors
    }

    // --- Git Status Short Format Enum ---

    public enum SandboxGitStatusShortFormat
    {
        None, // ""
        M,
        A,
        D,
        R,
        C,
        U,
        Unknown // "?"
    }

    // --- Git Item & Changed Files ---

    public class SandboxGitItem
    {
        public string Path { get; set; }
        public SandboxGitStatusShortFormat Index { get; set; }
        public SandboxGitStatusShortFormat WorkingTree { get; set; }
        public bool IsStaged { get; set; }
        public bool IsConflicted { get; set; }
        public string FileId { get; set; }
    }

    public class SandboxGitChangedFiles : Dictionary<string, SandboxGitItem> { }

    // --- Branch Properties ---

    public class SandboxGitBranchProperties
    {
        public string Head { get; set; }
        public string Branch { get; set; }
        public int Ahead { get; set; }
        public int Behind { get; set; }
        public bool Safe { get; set; }
    }

    // --- Commit ---

    public class SandboxGitCommit
    {
        public string Hash { get; set; }
        public string Date { get; set; }
        public string Message { get; set; }
        public string Author { get; set; }
    }

    // --- Git Status ---

    public class SandboxGitStatus
    {
        public SandboxGitChangedFiles ChangedFiles { get; set; }
        public List<SandboxGitItem> DeletedFiles { get; set; }
        public bool Conflicts { get; set; }
        public bool LocalChanges { get; set; }
        public SandboxGitBranchProperties Remote { get; set; }
        public SandboxGitBranchProperties Target { get; set; }
        public string Head { get; set; }
        public List<SandboxGitCommit> Commits { get; set; }
        public string Branch { get; set; }
        public bool IsMerging { get; set; }
    }

    // --- Git Target Diff ---

    public class SandboxGitTargetDiff
    {
        public int Ahead { get; set; }
        public int Behind { get; set; }
        public List<SandboxGitCommit> Commits { get; set; }
    }

    // --- Git Remotes ---

    public class SandboxGitRemotes
    {
        public string Origin { get; set; }
        public string Upstream { get; set; }
    }

    // --- Git Remote Params ---

    public class SandboxGitRemoteParams
    {
        public string Reference { get; set; }
        public string Path { get; set; }
    }

    // --- Git Diff Status Params ---

    public class SandboxGitDiffStatusParams
    {
        public string Base { get; set; }
        public string Head { get; set; }
    }

    // --- Git Diff Status ---

    public class SandboxGitDiffStatusItem
    {
        public SandboxGitStatusShortFormat Status { get; set; }
        public string Path { get; set; }
        public string OldPath { get; set; }
        public List<SandboxGitDiffHunk> Hunks { get; set; }
    }

    public class SandboxGitDiffHunk
    {
        public SandboxGitDiffRange Original { get; set; }
        public SandboxGitDiffRange Modified { get; set; }
    }

    public class SandboxGitDiffRange
    {
        public int Start { get; set; }
        public int End { get; set; }
    }

    public class SandboxGitDiffStatusResult
    {
        public List<SandboxGitDiffStatusItem> Files { get; set; }
    }
}