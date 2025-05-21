using System.Collections.Generic;

namespace CodeSandbox.SDK.Models
{
    /// <summary>
    /// Parameters for searching paths matching a pattern.
    /// </summary>
    public class PathSearchParams
    {
        /// <summary>
        /// Gets or sets the search pattern.
        /// </summary>
        public string Pattern { get; set; }
    }

    /// <summary>
    /// Result of a path search operation.
    /// </summary>
    public class PathSearchResult
    {
        /// <summary>
        /// Gets or sets the list of matching paths.
        /// </summary>
        public List<string> Paths { get; set; }
    }

    /// <summary>
    /// Request model for uploading a file.
    /// </summary>
    public class UploadRequest
    {
        /// <summary>
        /// Gets or sets the parent directory ID where the file should be uploaded.
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// Gets or sets the filename for the upload.
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets the file content as a string.
        /// </summary>
        public string Content { get; set; }
    }

    /// <summary>
    /// Result of a file upload operation.
    /// </summary>
    public class UploadResult
    {
        /// <summary>
        /// Gets or sets the ID of the uploaded file.
        /// </summary>
        public string FileId { get; set; }
    }

    /// <summary>
    /// Request model for downloading files.
    /// </summary>
    public class DownloadRequest
    {
        /// <summary>
        /// Gets or sets the path to download.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the list of paths to exclude from download.
        /// </summary>
        public List<string> Excludes { get; set; }
    }

    /// <summary>
    /// Result of a download operation.
    /// </summary>
    public class DownloadResult
    {
        /// <summary>
        /// Gets or sets the URL to download the requested content.
        /// </summary>
        public string DownloadUrl { get; set; }
    }

    /// <summary>
    /// Parameters for reading a file from the filesystem.
    /// </summary>
    public class FSReadFileParams
    {
        /// <summary>
        /// Gets or sets the path of the file to read.
        /// </summary>
        public string Path { get; set; }
    }

    /// <summary>
    /// Result of reading a file's content.
    /// </summary>
    public class FSReadFileResult
    {
        /// <summary>
        /// Gets or sets the content of the file.
        /// </summary>
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
