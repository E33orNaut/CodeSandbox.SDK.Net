using System.Collections.Generic;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents a collection of changed Git files, mapping file paths to their corresponding <see cref="GitItem"/> details.
    /// </summary>
    public class GitChangedFiles : Dictionary<string, GitItem>
    {
    }
}
