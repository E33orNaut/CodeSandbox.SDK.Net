using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodeSandbox.SDK.Models
{
    /// <summary>
    /// Parameters for searching paths matching a pattern.
    /// </summary>
    public class PathSearchParams
    {
        [JsonProperty("pattern")]
        public string Pattern { get; set; }
    }

    /// <summary>
    /// Result of a path search operation.
    /// </summary>
    public class PathSearchResult
    {
        [JsonProperty("paths")]
        public List<string> Paths { get; set; }
    }

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
    /// Result of a download operation.
    /// </summary>
    public class DownloadResult
    {
        [JsonProperty("downloadUrl")]
        public string DownloadUrl { get; set; }
    }

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

    /// <summary>
    /// Parameters for reading a directory.
    /// </summary>
    public class FSReadDirParams
    {
        /// <summary>
        /// Gets or sets the path of the directory to read.
        /// </summary>
        public string Path { get; set; }
    }

    /// <summary>
    /// Result of reading a directory.
    /// </summary>
    public class FSReadDirResult
    {
        /// <summary>
        /// Gets or sets the entries (files/directories) in the directory.
        /// </summary>
        public FileInfo[] Entries { get; set; }
    }

    /// <summary>
    /// Information about a file or directory.
    /// </summary>
    public class FileInfo
    {
        /// <summary>
        /// Gets or sets the name of the file or directory.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this entry is a directory.
        /// </summary>
        public bool IsDirectory { get; set; }

        /// <summary>
        /// Gets or sets the size of the file in bytes.
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// Gets or sets the last modification timestamp.
        /// </summary>
        public string ModifiedAt { get; set; }
    }

    /// <summary>
    /// Parameters for retrieving filesystem statistics.
    /// </summary>
    public class FSStatParams
    {
        /// <summary>
        /// Gets or sets the path to retrieve stats for.
        /// </summary>
        public string Path { get; set; }
    }

    /// <summary>
    /// Result of a filesystem stat operation.
    /// </summary>
    public class FSStatResult
    {
        /// <summary>
        /// Gets or sets the path the stats correspond to.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the path is a directory.
        /// </summary>
        public bool IsDirectory { get; set; }

        /// <summary>
        /// Gets or sets the size in bytes.
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// Gets or sets the creation timestamp.
        /// </summary>
        public string CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the last modification timestamp.
        /// </summary>
        public string ModifiedAt { get; set; }
    }

    /// <summary>
    /// Parameters for copying a file or directory.
    /// </summary>
    public class FSCopyParams
    {
        /// <summary>
        /// Gets or sets the source path to copy from.
        /// </summary>
        public string SourcePath { get; set; }

        /// <summary>
        /// Gets or sets the destination path to copy to.
        /// </summary>
        public string DestinationPath { get; set; }
    }

    /// <summary>
    /// Parameters for renaming a file or directory.
    /// </summary>
    public class FSRenameParams
    {
        /// <summary>
        /// Gets or sets the source path to rename.
        /// </summary>
        public string SourcePath { get; set; }

        /// <summary>
        /// Gets or sets the destination path name.
        /// </summary>
        public string DestinationPath { get; set; }
    }

    /// <summary>
    /// Parameters for removing a file or directory.
    /// </summary>
    public class FSRemoveParams
    {
        /// <summary>
        /// Gets or sets the path to remove.
        /// </summary>
        public string Path { get; set; }
    }

    /// <summary>
    /// Parameters for creating a new directory.
    /// </summary>
    public class FSMkdirParams
    {
        /// <summary>
        /// Gets or sets the path where the directory will be created.
        /// </summary>
        public string Path { get; set; }
    }

    /// <summary>
    /// Parameters for watching a directory or file for changes.
    /// </summary>
    public class FSWatchParams
    {
        /// <summary>
        /// Gets or sets the path to watch.
        /// </summary>
        public string Path { get; set; }
    }

    /// <summary>
    /// Parameters for stopping to watch a directory or file.
    /// </summary>
    public class FSUnwatchParams
    {
        /// <summary>
        /// Gets or sets the path to stop watching.
        /// </summary>
        public string Path { get; set; }
    }

    /// <summary>
    /// Result returned when a watch is created.
    /// </summary>
    public class FSWatchResult
    {
        /// <summary>
        /// Gets or sets the watch ID.
        /// </summary>
        public string WatchId { get; set; }
    }
}
