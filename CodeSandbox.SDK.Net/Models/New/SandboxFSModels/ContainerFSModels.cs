using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models.New.SandboxFSModels
{
    // --- /fs/read ---
    public class FSReadResult
    {
        [JsonProperty("treeNodes")]
        public List<object> TreeNodes { get; set; } // Use a specific type if you have a node model
        [JsonProperty("clock")]
        public long Clock { get; set; }
    }

    // --- /fs/operation ---
    public class FSOperationRequest
    {
        [JsonProperty("operation")]
        public FSOperation Operation { get; set; }
    }

    public class FSOperation
    {
        [JsonProperty("type")]
        public string Type { get; set; } // "create", "delete", "move"
        // For "create"
        [JsonProperty("parentId")]
        public string ParentId { get; set; }
        [JsonProperty("newEntry")]
        public FSOperationNewEntry NewEntry { get; set; }
        // For "delete" and "move"
        [JsonProperty("id")]
        public string Id { get; set; }
        // For "move"
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class FSOperationNewEntry
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; } // "directory" or "file"
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class FSOperationResult
    {
        [JsonProperty("code")]
        public int Code { get; set; } // 0 = success, 1 = ignored
        [JsonProperty("clock")]
        public long? Clock { get; set; }
    }

    // --- /fs/search ---
    public class FSSearchParams
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

    public class SearchResult
    {
        [JsonProperty("fileId")]
        public string FileId { get; set; }
        [JsonProperty("lines")]
        public SearchResultLine Lines { get; set; }
        [JsonProperty("lineNumber")]
        public int LineNumber { get; set; }
        [JsonProperty("absoluteOffset")]
        public int AbsoluteOffset { get; set; }
        [JsonProperty("submatches")]
        public List<SearchSubMatch> Submatches { get; set; }
    }

    public class SearchResultLine
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class SearchSubMatch
    {
        [JsonProperty("match")]
        public SearchResultLine Match { get; set; }
        [JsonProperty("start")]
        public int Start { get; set; }
        [JsonProperty("end")]
        public int End { get; set; }
    }

    // --- /fs/streamingSearch ---
    public class FSStreamingSearchParams
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

    public class FSStreamingSearchResult
    {
        [JsonProperty("searchId")]
        public string SearchId { get; set; }
    }

    // --- /fs/cancelStreamingSearch ---
    public class FSCancelStreamingSearchParams
    {
        [JsonProperty("searchId")]
        public string SearchId { get; set; }
    }

    public class FSCancelStreamingSearchResult
    {
        [JsonProperty("searchId")]
        public string SearchId { get; set; }
    }

    // --- /fs/mkdir ---
    public class FSMkdirParams
    {
        [JsonProperty("path")]
        public string Path { get; set; }
        [JsonProperty("recursive")]
        public bool? Recursive { get; set; }
    }

    // --- /fs/watch ---
    public class FSWatchParams
    {
        [JsonProperty("path")]
        public string Path { get; set; }
        [JsonProperty("recursive")]
        public bool? Recursive { get; set; }
        [JsonProperty("excludes")]
        public List<string> Excludes { get; set; }
    }

    public class FSWatchResult
    {
        [JsonProperty("watchId")]
        public string WatchId { get; set; }
    }

    // --- /fs/unwatch ---
    public class FSUnwatchParams
    {
        [JsonProperty("watchId")]
        public string WatchId { get; set; }
    }
}