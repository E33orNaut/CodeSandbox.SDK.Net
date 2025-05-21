namespace CodeSandbox.SDK.Models
{
    public class SuccessResponse<T>
    {
        public string Status { get; set; }
        public T Result { get; set; }
    }
}
