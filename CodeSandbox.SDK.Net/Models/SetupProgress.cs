using System.Collections.Generic;

namespace CodeSandbox.SDK.Net.Models
{
    public enum SetupState
    {
        IDLE,
        IN_PROGRESS,
        FINISHED,
        STOPPED
    }

    public class SetupProgress
    {
        public SetupState State { get; set; }

        public List<Step> Steps { get; set; } = new List<Step>();

        public int CurrentStepIndex { get; set; }
    }
}
