namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents the status and progress information of a task.
    /// </summary>
    public class TaskDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the task.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the current status of the task.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the progress of the task, represented as a value between 0 and 1.
        /// </summary>
        public double Progress { get; set; }
    }
}
