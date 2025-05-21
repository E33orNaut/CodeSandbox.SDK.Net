namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents the response from a container setup request.
    /// </summary>
    public class ContainerSetupResponse
    {
        /// <summary>
        /// Gets or sets the status code of the response.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the task details resulting from the container setup.
        /// </summary>
        public TaskDTO Result { get; set; }
    }
}
