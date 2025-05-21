using System.Collections.Generic;

namespace CodeSandbox.SDK.Net.Models
{
    public class ContainerSetupRequest
    {
        public string ContainerId;

        public string TemplateId { get; set; }
        public Dictionary<string, string> TemplateArgs { get; set; }
        public List<ContainerSetupFeature> Features { get; set; }  
    }
}
