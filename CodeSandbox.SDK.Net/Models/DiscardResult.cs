using System.Collections.Generic;

namespace CodeSandbox.SDK.Net.Models
{
    public class DiscardResult
    {
        public int Status { get; set; }
        public DiscardResultData Result { get; set; }
    }

    public class DiscardResultData
    {
        public List<string> Paths { get; set; }
    }
}
