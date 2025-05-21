using System.Collections.Generic;

namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents a request to set up a container.
    /// </summary>
    public class ContainerSetupRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier of the container.
        /// </summary>
        public string ContainerId { get; set; }

        /// <summary>
        /// Gets or sets the template identifier used for the container setup.
        /// </summary>
        public string TemplateId { get; set; }

        /// <summary>
        /// Gets or sets the template arguments as key-value pairs.
        /// These arguments customize the template behavior during setup.
        /// </summary>
        public Dictionary<string, string> TemplateArgs { get; set; }

        /// <summary>
        /// Gets or sets the list of features to be applied during container setup.
        /// </summary>
        public List<ContainerSetupFeature> Features { get; set; }
    }
}
