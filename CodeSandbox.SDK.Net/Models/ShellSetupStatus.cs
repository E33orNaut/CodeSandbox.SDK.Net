using Newtonsoft.Json;
using System.Runtime.Serialization;

    namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents the status of a setup shell step.
    /// </summary>
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum SetupShellStatus
    {
        /// <summary>
        /// The step completed successfully.
        /// </summary>
        [EnumMember(Value = "SUCCEEDED")]
        Succeeded,

        /// <summary>
        /// The step failed.
        /// </summary>
        [EnumMember(Value = "FAILED")]
        Failed,

        /// <summary>
        /// The step was skipped.
        /// </summary>
        [EnumMember(Value = "SKIPPED")]
        Skipped
    }
}
