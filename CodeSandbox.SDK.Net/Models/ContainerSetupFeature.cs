using System.Collections.Generic;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents a feature used during container setup.
    /// </summary>
    public class ContainerSetupFeature
    {
        /// <summary>
        /// Gets or sets the unique identifier of the feature.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the options associated with the feature.
        /// The dictionary contains option names as keys and their corresponding values.
        /// </summary>
        public Dictionary<string, string> Options { get; set; }
    }
}
