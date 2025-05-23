using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models.New.SandboxFSModels
{
    // --- Common Response Models ---

    /// <summary>
    /// Generic success response wrapper for FS API.
    /// </summary>
    public class SandboxFSSuccessResponse<T>
    {
        public int Status { get; set; }
        public T Result { get; set; }
    }

    public class SandboxFSSuccessResponse
    {
        public int Status { get; set; }
        public object Result { get; set; }
    }

    public class SandboxFSErrorResponse
    {
        public int Status { get; set; }
        public SandboxFSErrorUnion Error { get; set; }
    }

    // --- Error Models ---

    public class SandboxFSDefaultError
    {
        public SandboxFSPitcherErrorCode Code { get; set; }
        public object Data { get; set; }
        public string PublicMessage { get; set; }
    }

    public class SandboxFSRawFsError
    {
        public int Code { get; set; } // always 102             
        public SandboxFSRawFsErrorData Data { get; set; }
        public string PublicMessage { get; set; }
    }

    public class SandboxFSRawFsErrorData
    {
        public int? Errno { get; set; }
    }

    public enum SandboxFSPitcherErrorCode
    {
        CRITICAL_ERROR = 0,
        FEATURE_UNAVAILABLE = 1,
        NO_ACCESS = 2,
        RATE_LIMIT = 3,
        INVALID_ID = 100,
        INVALID_PATH = 101,
        RAWFS_ERROR = 102,
        SHELL_NOT_ACCESSIBLE = 200,
        SHELL_CLOSED = 201,
        SHELL_NOT_FOUND = 204,
        MODEL_NOT_FOUND = 300,
        GIT_OPERATION_IN_PROGRESS = 400,
        GIT_REMOTE_FILE_NOT_FOUND = 404,
        GIT_FETCH_FAIL = 410,
        GIT_PULL_CONFLICT = 420,
        GIT_RESET_LOCAL_REMOTE_ERROR = 430,
        GIT_PUSH_FAIL = 440,
        GIT_RESET_CHECKOUT_INITIAL_BRANCH_FAIL = 450,
        GIT_PULL_FAIL = 460,
        GIT_TRANSPOSE_LINES_FAIL = 470,
        CHANNEL_NOT_FOUND = 500,
        CONFIG_FILE_ALREADY_EXISTS = 600,
        TASK_NOT_FOUND = 601,
        COMMAND_ALREADY_CONFIGURED = 602,
        COMMAND_NOT_FOUND = 704,
        AI_NOT_AVAILABLE = 800,
        PROMPT_TOO_BIG = 801,
        FAILED_TO_RESPOND = 802,
        AI_TOO_FREQUENT_REQUESTS = 803,
        AI_CHAT_NOT_FOUND = 814
    }

    // Discriminated union for error responses
    public class SandboxFSErrorUnion
    {
        public SandboxFSDefaultError DefaultError { get; set; }
        public SandboxFSRawFsError RawFsError { get; set; }
    }

    // --- WriteFile ---

    /// <summary>
    /// Request model for writing a file.
    /// </summary>
    public class SandboxFSWriteFileRequest
    {
        public string Path { get; set; }
        public string Content { get; set; }
        public bool? Create { get; set; }
        public bool? Overwrite { get; set; }
    }

    // --- FSRead ---

    public class SandboxFSReadResult
    {
        public List<object> TreeNodes { get; set; }
        public double Clock { get; set; }
    }

    // --- FSOperation ---

    public class SandboxFSOperationRequest
    {
        public SandboxFSOperation Operation { get; set; }
    }

    public class SandboxFSOperation
    {
        // Use one property at a time, depending on the operation type
        public SandboxFSCreateOperation Create { get; set; }
        public SandboxFSDeleteOperation Delete { get; set; }
        public SandboxFSMoveOperation Move { get; set; }
    }

    public class SandboxFSCreateOperation
    {
        public string Type { get; set; } // "create"
        public string ParentId { get; set; }
        public SandboxFSNewEntry NewEntry { get; set; }
    }

    public class SandboxFSNewEntry
    {
        public string Id { get; set; }
        public string Type { get; set; } // "directory" or "file"
        public string Name { get; set; }
    }

    public class SandboxFSDeleteOperation
    {
        public string Type { get; set; } // "delete"
        public string Id { get; set; }
    }

    public class SandboxFSMoveOperation
    {
        public string Type { get; set; } // "move"
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Name { get; set; }
    }

    public class SandboxFSOperationResultSuccess
    {
        public int Code { get; set; } // 0
        public double Clock { get; set; }
    }

    public class SandboxFSOperationResultIgnored
    {
        public int Code { get; set; } // 1
    }

    // --- FSSearch ---

    public class SandboxFSSearchParams
    {
        public string Text { get; set; }
        public string Glob { get; set; }
        public bool? IsRegex { get; set; }
        public string CaseSensitivity { get; set; }
    }

    public class SandboxFSSearchResult
    {
        public string FileId { get; set; }
        public SandboxFSSearchLine Lines { get; set; }
        public int LineNumber { get; set; }
        public int AbsoluteOffset { get; set; }
        public List<SandboxFSSearchSubMatch> Submatches { get; set; }
    }

    public class SandboxFSSearchLine
    {
        public string Text { get; set; }
    }

    public class SandboxFSSearchSubMatch
    {
        public SandboxFSSearchMatch Match { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
    }

    public class SandboxFSSearchMatch
    {
        public string Text { get; set; }
    }

    // --- FSStreamingSearch ---

    public class SandboxFSStreamingSearchParams
    {
        public string SearchId { get; set; }
        public string Text { get; set; }
        public string Glob { get; set; }
        public bool? IsRegex { get; set; }
        public string CaseSensitivity { get; set; }
        public int? MaxResults { get; set; }
    }

    public class SandboxFSStreamingSearchResult
    {
        public string SearchId { get; set; }
    }

    // --- CancelStreamingSearch ---

    public class SandboxFSCancelStreamingSearchRequest
    {
        public string SearchId { get; set; }
    }

    public class SandboxFSCancelStreamingSearchResult
    {
        public string SearchId { get; set; }
    }

    // --- PathSearch ---

    /// <summary>
    /// Request model for searching paths.
    /// </summary>
    public class SandboxFSPathSearchRequest
    {
        [JsonProperty("pattern")]
        public string Pattern { get; set; }
    }

    /// <summary>
    /// Result model for path search.
    /// </summary>
    public class SandboxFSPathSearchResult
    {
        public List<SandboxFSPathSearchMatch> Matches { get; set; }
    }

    /// <summary>
    /// Represents a single match in a path search result.
    /// </summary>
    public class SandboxFSPathSearchMatch
    {
        public string Path { get; set; }
        public int? Line { get; set; }
        public int? Column { get; set; }
        public string Preview { get; set; }
    }

    // --- InvalidIdError ---

    public class SandboxFSInvalidIdError
    {
        public int Code { get; set; } // 100
    }

    // --- FSReadFile ---

    /// <summary>
    /// Parameters for reading a file from the filesystem.
    /// </summary>
    public class FSReadFileParams
    {
        [JsonProperty("path")]
        public string Path { get; set; }
    }

    /// <summary>
    /// Result of reading a file's content.
    /// </summary>
    public class FSReadFileResult
    {
        [JsonProperty("content")]
        public string Content { get; set; }
    }

    // --- FSReadDir ---

    /// <summary>
    /// Parameters for reading a directory.
    /// </summary>
    public class FSReadDirParams
    {
        public string Path { get; set; }
    }

    /// <summary>
    /// Result of reading a directory.
    /// </summary>
    public class FSReadDirResult
    {
        public FileInfo[] Entries { get; set; }
    }

    public class SandboxFSReadDirEntry
    {
        public string Name { get; set; }
        public string Type { get; set; } // "directory" or "file"
        public bool IsSymlink { get; set; }
    }

    // --- FSStat ---

    /// <summary>
    /// Parameters for retrieving filesystem statistics.
    /// </summary>
    public class FSStatParams
    {
        public string Path { get; set; }
    }

    /// <summary>
    /// Result of a filesystem stat operation.
    /// </summary>
    public class FSStatResult
    {
        public string Path { get; set; }
        public bool IsDirectory { get; set; }
        public long Size { get; set; }
        public string CreatedAt { get; set; }
        public string ModifiedAt { get; set; }
    }

    public class SandboxFSStatParams
    {
        public string Path { get; set; }
    }

    public class SandboxFSStatResult
    {
        public string Type { get; set; }
        public bool IsSymlink { get; set; }
        public int Size { get; set; }
        public int Mtime { get; set; }
        public int Ctime { get; set; }
        public int Atime { get; set; }
    }

    // --- FSCopy ---

    /// <summary>
    /// Parameters for copying a file or directory.
    /// </summary>
    public class FSCopyParams
    {
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
    }

    public class SandboxFSCopyParams
    {
        public string From { get; set; }
        public string To { get; set; }
        public bool? Recursive { get; set; }
        public bool? Overwrite { get; set; }
    }

    // --- FSRename ---

    /// <summary>
    /// Parameters for renaming a file or directory.
    /// </summary>
    public class FSRenameParams
    {
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
    }

    public class SandboxFSRenameParams
    {
        public string From { get; set; }
        public string To { get; set; }
        public bool? Overwrite { get; set; }
    }

    // --- FSRemove ---

    public class SandboxFSRemoveParams
    {
        public string Path { get; set; }
        public bool? Recursive { get; set; }
    }

    /// <summary>
    /// Parameters for removing a file or directory.
    /// </summary>
    public class FSRemoveParams
    {
        public string Path { get; set; }
    }

    // --- FSMkdir ---

    public class SandboxFSMkdirParams
    {
        public string Path { get; set; }
        public bool? Recursive { get; set; }
    }

    // --- FSWatch ---

    public class SandboxFSWatchParams
    {
        public string Path { get; set; }
        public bool? Recursive { get; set; }
        public List<string> Excludes { get; set; }
    }

    public class SandboxFSWatchResult
    {
        public string WatchId { get; set; }
    }

    // --- FSUnwatch ---

    public class SandboxFSUnwatchParams
    {
        public string WatchId { get; set; }
    }

    // --- Upload ---

    /// <summary>
    /// Request model for uploading a file.
    /// </summary>
    public class UploadRequest
    {
        [JsonProperty("parentId")]
        public string ParentId { get; set; }

        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }

    /// <summary>
    /// Result of a file upload operation.
    /// </summary>
    public class UploadResult
    {
        [JsonProperty("fileId")]
        public string FileId { get; set; }
    }

    // --- Download ---

    /// <summary>
    /// Request model for downloading files.
    /// </summary>
    public class DownloadRequest
    {
        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("excludes")]
        public List<string> Excludes { get; set; }
    }

    /// <summary>
    /// Result of a file download operation.
    /// </summary>
    public class DownloadResult
    {
        [JsonProperty("downloadUrl")]
        public string DownloadUrl { get; set; }
    }

    /// <summary>
    /// Represents a file or directory entry in a directory listing.
    /// </summary>
    public class FileInfo
    {
        public string Name { get; set; }
        public bool IsDirectory { get; set; }
        public long Size { get; set; }
        public string CreatedAt { get; set; }
        public string ModifiedAt { get; set; }
    }
}