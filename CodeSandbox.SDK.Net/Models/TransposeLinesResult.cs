using System.Collections.Generic;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents the result of a transpose lines operation.
    /// </summary>
    public class TransposeLinesResult
    {
        /// <summary>
        /// Gets or sets the status code of the operation.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the list of transpose line result items.
        /// </summary>
        public List<TransposeLineResultItem> Result { get; set; }
    }

    /// <summary>
    /// Represents a single transpose line result item with its file path and line number.
    /// </summary>
    public class TransposeLineResultItem
    {
        /// <summary>
        /// Gets or sets the file path of the transposed line.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the line number of the transposed line.
        /// </summary>
        public int Line { get; set; }
    }
}
