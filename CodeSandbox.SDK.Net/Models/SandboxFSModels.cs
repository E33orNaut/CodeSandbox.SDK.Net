using System.Collections.Generic;

namespace CodeSandbox.SDK.Models
{
    public class PathSearchParams
    {
        public string Pattern { get; set; }
    }

    public class PathSearchResult
    {
        public List<string> Paths { get; set; }
    }

    public class UploadRequest
    {
        public string ParentId { get; set; }
        public string Filename { get; set; }
        public string Content { get; set; }
    }

    public class UploadResult
    {
        public string FileId { get; set; }
    }

    public class DownloadRequest
    {
        public string Path { get; set; }
        public List<string> Excludes { get; set; }
    }

    public class DownloadResult
    {
        public string DownloadUrl { get; set; }
    }

    public class FSReadFileParams
    {
        public string Path { get; set; }
    }

    public class FSReadFileResult
    {
        public string Content { get; set; }
    }

     
    public class FSReadDirParams
    {
        public string Path { get; set; }
    }

    public class FSReadDirResult
    {
        public FileInfo[] Entries { get; set; }
    }

    public class FileInfo
    {
        public string Name { get; set; }
        public bool IsDirectory { get; set; }
        public long Size { get; set; }
        public string ModifiedAt { get; set; } 
    }
     
    public class FSStatParams
    {
        public string Path { get; set; }
    }

    public class FSStatResult
    {
        public string Path { get; set; }
        public bool IsDirectory { get; set; }
        public long Size { get; set; }
        public string CreatedAt { get; set; }
        public string ModifiedAt { get; set; }
    }
     
    public class FSCopyParams
    {
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
    }
     
    public class FSRenameParams
    {
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
    }
     
    public class FSRemoveParams
    {
        public string Path { get; set; }
    }

    public class FSMkdirParams
    {
        public string Path { get; set; }
    }

    public class FSWatchParams
    {
        public string Path { get; set; }
    }

    public class FSUnwatchParams
    {
        public string Path { get; set; }
    }

    public class FSWatchResult
    {
        public string WatchId { get; set; }
    }


}
