using System.Collections.Generic;

namespace Models.New.Git
{
    public class GitDiffStatusRequest
    {
        public string Base { get; set; }
        public string Head { get; set; }
    }

    public class GitDiffStatusResponse
    {
        public int Status { get; set; }
        public GitDiffStatusResult Result { get; set; }
    }

    public class GitDiffStatusResult
    {
        public List<GitDiffStatusFile> Files { get; set; }
    }

    public class GitDiffStatusFile
    {
        public GitDiffStatusShortFormat Status { get; set; }
        public string Path { get; set; }
        public string OldPath { get; set; }
        public List<GitDiffStatusHunk> Hunks { get; set; }
    }

    public enum GitDiffStatusShortFormat
    {
        None,
        M,
        A,
        D,
        R,
        C,
        U,
        Unknown
    }

    public class GitDiffStatusHunk
    {
        public GitDiffStatusRange Original { get; set; }
        public GitDiffStatusRange Modified { get; set; }
    }

    public class GitDiffStatusRange
    {
        public int Start { get; set; }
        public int End { get; set; }
    }

    public class GitDiffStatusErrorResponse
    {
        public int Status { get; set; }
        public GitDiffStatusCommonError Error { get; set; }
    }

    public class GitDiffStatusCommonError
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}