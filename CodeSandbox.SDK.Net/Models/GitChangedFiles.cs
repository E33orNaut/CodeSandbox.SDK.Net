using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents a map of file IDs to Git items (changed files).
    /// </summary>
    public class GitChangedFiles : Dictionary<string, GitItem>
    {
    }
}
