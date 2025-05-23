using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents a file or item in a Git repository.
    /// </summary>
    public class GitItem
    {
        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the index status of the file.
        /// Possible values include: "", "M" (modified), "A" (added), "D" (deleted), etc.
        /// </summary>
        [JsonProperty("index")]
        public string Index { get; set; }

        /// <summary>
        /// Gets or sets the working tree status of the file.
        /// </summary>
        [JsonProperty("workingTree")]
        public string WorkingTree { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the file is staged for commit.
        /// </summary>
        [JsonProperty("isStaged")]
        public bool IsStaged { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the file has conflicts.
        /// </summary>
        [JsonProperty("isConflicted")]
        public bool IsConflicted { get; set; }

        /// <summary>
        /// Gets or sets the unique file identifier.
        /// </summary>
        [JsonProperty("fileId")]
        public string FileId { get; set; }
    }
}
