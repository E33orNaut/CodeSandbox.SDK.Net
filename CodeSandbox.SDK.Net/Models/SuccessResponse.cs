namespace CodeSandbox.SDK.Models
{
    /// <summary>
    /// Represents a successful response wrapping a result of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the response result.</typeparam>
    public class SuccessResponse<T>
    {
        /// <summary>
        /// Gets or sets the status of the response as a string.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the result of the response.
        /// </summary>
        public T Result { get; set; }
    }
}
