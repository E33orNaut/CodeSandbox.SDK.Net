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
        /// <summary>
        /// Status code for successful operations.
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        /// Result payload for the operation.
        /// </summary>
        [JsonProperty("result")]
        public T Result { get; set; }
    }

    /// <summary>
    /// Non-generic success response wrapper for FS API.
    /// </summary>
    public class SandboxFSSuccessResponse
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("result")]
        public object Result { get; set; }
    }

    /// <summary>
    /// Error response wrapper for FS API.
    /// </summary>
    public class SandboxFSErrorResponse
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("error")]
        public SandboxFSErrorUnion Error { get; set; }
    }

    // --- Error Models ---

    /// <summary>
    /// Default error details for FS API.
    /// </summary>
    public class SandboxFSDefaultError
    {
        [JsonProperty("code")]
        public SandboxFSPitcherErrorCode Code { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; }

        [JsonProperty("publicMessage")]
        public string PublicMessage { get; set; }
    }

    /// <summary>
    /// Raw FS error details for FS API.
    /// </summary>
    public class SandboxFSRawFsError
    {
        [JsonProperty("code")]
        public int Code { get; set; } // always 102             

        [JsonProperty("data")]
        public SandboxFSRawFsErrorData Data { get; set; }

        [JsonProperty("publicMessage")]
        public string PublicMessage { get; set; }
    }

    /// <summary>
    /// Data for raw FS error.
    /// </summary>
    public class SandboxFSRawFsErrorData
    {
        [JsonProperty("errno")]
        public int? Errno { get; set; }
    }

    /// <summary>
    /// Error codes for FS API.
    /// </summary>
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

    /// <summary>
    /// Discriminated union for error responses.
    /// </summary>
    public class SandboxFSErrorUnion
    {
        [JsonProperty("defaultError")]
        public SandboxFSDefaultError DefaultError { get; set; }

        [JsonProperty("rawFsError")]
        public SandboxFSRawFsError RawFsError { get; set; }
    }

    // --- WriteFile ---

    /// <summary>
    /// Request model for writing a file.
    /// </summary>
    public class SandboxFSWriteFileRequest
    {
        /// <summary>
        /// Path to the file.
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }

        /// <summary>
        /// Content to write to the file.
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// Whether to create the file if it does not exist.
        /// </summary>
        [JsonProperty("create")]
        public bool? Create { get; set; }

        /// <summary>
        /// Whether to overwrite the file if it exists.
        /// </summary>
        [JsonProperty("overwrite")]
        public bool? Overwrite { get; set; }
    }

    // --- FSRead ---

    /// <summary>
    /// Result of reading a directory tree.
    /// </summary>
    public class SandboxFSReadResult
    {
        [JsonProperty("treeNodes")]
        public List<object> TreeNodes { get; set; }

        [JsonProperty("clock")]
        public double Clock { get; set; }
    }

    // --- FSOperation ---

    /// <summary>
    /// Request for a filesystem operation.
    /// </summary>
    public class SandboxFSOperationRequest
    {
        [JsonProperty("operation")]
        public SandboxFSOperation Operation { get; set; }
    }

    /// <summary>
    /// Represents a filesystem operation (create, delete, move).
    /// </summary>
    public class SandboxFSOperation
    {
        [JsonProperty("create")]
        public SandboxFSCreateOperation Create { get; set; }

        [JsonProperty("delete")]
        public SandboxFSDeleteOperation Delete { get; set; }

        [JsonProperty("move")]
        public SandboxFSMoveOperation Move { get; set; }
    }

    /// <summary>
    /// Create operation for the filesystem.
    /// </summary>
    public class SandboxFSCreateOperation
    {
        [JsonProperty("type")]
        public string Type { get; set; } // "create"

        [JsonProperty("parentId")]
        public string ParentId { get; set; }

        [JsonProperty("newEntry")]
        public SandboxFSNewEntry NewEntry { get; set; }
    }

    /// <summary>
    /// Represents a new entry (file or directory) to create.
    /// </summary>
    public class SandboxFSNewEntry
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; } // "directory" or "file"

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    /// <summary>
    /// Delete operation for the filesystem.
    /// </summary>
    public class SandboxFSDeleteOperation
    {
        [JsonProperty("type")]
        public string Type { get; set; } // "delete"

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    /// <summary>
    /// Move operation for the filesystem.
    /// </summary>
    public class SandboxFSMoveOperation
    {
        [JsonProperty("type")]
        public string Type { get; set; } // "move"

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("parentId")]
        public string ParentId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    /// <summary>
    /// Success result for a filesystem operation.
    /// </summary>
    public class SandboxFSOperationResultSuccess
    {
        [JsonProperty("code")]
        public int Code { get; set; } // 0

        [JsonProperty("clock")]
        public double Clock { get; set; }
    }

    /// <summary>
    /// Ignored result for a filesystem operation.
    /// </summary>
    public class SandboxFSOperationResultIgnored
    {
        [JsonProperty("code")]
        public int Code { get; set; } // 1
    }

    // --- FSSearch ---

    /// <summary>
    /// Parameters for searching files.
    /// </summary>
    public class SandboxFSSearchParams
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("glob")]
        public string Glob { get; set; }

        [JsonProperty("isRegex")]
        public bool? IsRegex { get; set; }

        [JsonProperty("caseSensitivity")]
        public string CaseSensitivity { get; set; }
    }

    /// <summary>
    /// Result of a file search.
    /// </summary>
    public class SandboxFSSearchResult
    {
        [JsonProperty("fileId")]
        public string FileId { get; set; }

        [JsonProperty("lines")]
        public SandboxFSSearchLine Lines { get; set; }

        [JsonProperty("lineNumber")]
        public int LineNumber { get; set; }

        [JsonProperty("absoluteOffset")]
        public int AbsoluteOffset { get; set; }

        [JsonProperty("submatches")]
        public List<SandboxFSSearchSubMatch> Submatches { get; set; }
    }

    /// <summary>
    /// Represents a line in a file search result.
    /// </summary>
    public class SandboxFSSearchLine
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }

    /// <summary>
    /// Represents a submatch in a file search result.
    /// </summary>
    public class SandboxFSSearchSubMatch
    {
        [JsonProperty("match")]
        public SandboxFSSearchMatch Match { get; set; }

        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonProperty("end")]
        public int End { get; set; }
    }

    /// <summary>
    /// Represents a match in a file search result.
    /// </summary>
    public class SandboxFSSearchMatch
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }

    // --- FSStreamingSearch ---

    /// <summary>
    /// Parameters for streaming file search.
    /// </summary>
    public class SandboxFSStreamingSearchParams
    {
        [JsonProperty("searchId")]
        public string SearchId { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("glob")]
        public string Glob { get; set; }

        [JsonProperty("isRegex")]
        public bool? IsRegex { get; set; }

        [JsonProperty("caseSensitivity")]
        public string CaseSensitivity { get; set; }

        [JsonProperty("maxResults")]
        public int? MaxResults { get; set; }
    }

    /// <summary>
    /// Result of a streaming file search.
    /// </summary>
    public class SandboxFSStreamingSearchResult
    {
        [JsonProperty("searchId")]
        public string SearchId { get; set; }
    }

    // --- CancelStreamingSearch ---

    /// <summary>
    /// Request to cancel a streaming search.
    /// </summary>
    public class SandboxFSCancelStreamingSearchRequest
    {
        [JsonProperty("searchId")]
        public string SearchId { get; set; }
    }

    /// <summary>
    /// Result of canceling a streaming search.
    /// </summary>
    public class SandboxFSCancelStreamingSearchResult
    {
        [JsonProperty("searchId")]
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
        [JsonProperty("matches")]
        public List<SandboxFSPathSearchMatch> Matches { get; set; }
    }

    /// <summary>
    /// Represents a single match in a path search result.
    /// </summary>
    public class SandboxFSPathSearchMatch
    {
        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("line")]
        public int? Line { get; set; }

        [JsonProperty("column")]
        public int? Column { get; set; }

        [JsonProperty("preview")]
        public string Preview { get; set; }
    }

    // --- InvalidIdError ---

    /// <summary>
    /// Error for invalid ID.
    /// </summary>
    public class SandboxFSInvalidIdError
    {
        [JsonProperty("code")]
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
        [JsonProperty("path")]
        public string Path { get; set; }
    }

    /// <summary>
    /// Result of reading a directory.
    /// </summary>
    public class FSReadDirResult
    {
        [JsonProperty("entries")]
        public FileInfo[] Entries { get; set; }
    }

    /// <summary>
    /// Represents a file or directory entry in a directory listing.
    /// </summary>
    public class SandboxFSReadDirEntry
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; } // "directory" or "file"

        [JsonProperty("isSymlink")]
        public bool IsSymlink { get; set; }
    }

    // --- FSStat ---

    /// <summary>
    /// Parameters for retrieving filesystem statistics.
    /// </summary>
    public class FSStatParams
    {
        [JsonProperty("path")]
        public string Path { get; set; }
    }

    /// <summary>
    /// Result of a filesystem stat operation.
    /// </summary>
    public class FSStatResult
    {
        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("isDirectory")]
        public bool IsDirectory { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("modifiedAt")]
        public string ModifiedAt { get; set; }
    }

    /// <summary>
    /// Parameters for retrieving filesystem statistics (alternate).
    /// </summary>
    public class SandboxFSStatParams
    {
        [JsonProperty("path")]
        public string Path { get; set; }
    }

    /// <summary>
    /// Result of a filesystem stat operation (alternate).
    /// </summary>
    public class SandboxFSStatResult
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("isSymlink")]
        public bool IsSymlink { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("mtime")]
        public int Mtime { get; set; }

        [JsonProperty("ctime")]
        public int Ctime { get; set; }

        [JsonProperty("atime")]
        public int Atime { get; set; }
    }

    // --- FSCopy ---

    /// <summary>
    /// Parameters for copying a file or directory.
    /// </summary>
    public class FSCopyParams
    {
        [JsonProperty("sourcePath")]
        public string SourcePath { get; set; }

        [JsonProperty("destinationPath")]
        public string DestinationPath { get; set; }
    }

    /// <summary>
    /// Parameters for copying a file or directory (alternate).
    /// </summary>
    public class SandboxFSCopyParams
    {
        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("recursive")]
        public bool? Recursive { get; set; }

        [JsonProperty("overwrite")]
        public bool? Overwrite { get; set; }
    }

    // --- FSRename ---

    /// <summary>
    /// Parameters for renaming a file or directory.
    /// </summary>
    public class FSRenameParams
    {
        [JsonProperty("sourcePath")]
        public string SourcePath { get; set; }

        [JsonProperty("destinationPath")]
        public string DestinationPath { get; set; }
    }

    /// <summary>
    /// Parameters for renaming a file or directory (alternate).
    /// </summary>
    public class SandboxFSRenameParams
    {
        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("overwrite")]
        public bool? Overwrite { get; set; }
    }

    // --- FSRemove ---

    /// <summary>
    /// Parameters for removing a file or directory (alternate).
    /// </summary>
    public class SandboxFSRemoveParams
    {
        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("recursive")]
        public bool? Recursive { get; set; }
    }

    /// <summary>
    /// Parameters for removing a file or directory.
    /// </summary>
    public class FSRemoveParams
    {
        [JsonProperty("path")]
        public string Path { get; set; }
    }

    // --- FSMkdir ---

    /// <summary>
    /// Parameters for creating a directory.
    /// </summary>
    public class SandboxFSMkdirParams
    {
        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("recursive")]
        public bool? Recursive { get; set; }
    }

    // --- FSWatch ---

    /// <summary>
    /// Parameters for watching a file or directory.
    /// </summary>
    public class SandboxFSWatchParams
    {
        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("recursive")]
        public bool? Recursive { get; set; }

        [JsonProperty("excludes")]
        public List<string> Excludes { get; set; }
    }

    /// <summary>
    /// Result of watching a file or directory.
    /// </summary>
    public class SandboxFSWatchResult
    {
        [JsonProperty("watchId")]
        public string WatchId { get; set; }
    }

    // --- FSUnwatch ---

    /// <summary>
    /// Parameters for unwatching a file or directory.
    /// </summary>
    public class SandboxFSUnwatchParams
    {
        [JsonProperty("watchId")]
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
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isDirectory")]
        public bool IsDirectory { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("modifiedAt")]
        public string ModifiedAt { get; set; }
    }
}