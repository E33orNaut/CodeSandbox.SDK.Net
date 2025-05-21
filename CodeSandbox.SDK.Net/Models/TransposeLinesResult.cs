using System.Collections.Generic;

namespace CodeSandbox.SDK.Net.Models
{
    public class TransposeLinesResult
    {
        public int Status { get; set; }
        public List<TransposeLineResultItem> Result { get; set; }
    }

    public class TransposeLineResultItem
    {
        public string Path { get; set; }
        public int Line { get; set; }
    }
}
