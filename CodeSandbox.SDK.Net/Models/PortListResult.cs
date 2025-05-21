using System.Collections.Generic;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents the result containing a list of ports.
    /// </summary>
    public class PortListResult
    {
        /// <summary>
        /// Gets or sets the list of ports.
        /// </summary>
        public List<Port> List { get; set; }
    }
}
