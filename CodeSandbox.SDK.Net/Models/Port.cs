namespace CodeSandbox.SDK.Net.Models
{
    /// <summary>
    /// Represents a network port with its number and URL.
    /// </summary>
    public class Port
    {
        /// <summary>
        /// Gets or sets the port number.
        /// </summary>
        public int PortNumber { get; set; }

        /// <summary>
        /// Gets or sets the URL associated with the port.
        /// </summary>
        public string Url { get; set; }
    }
}
