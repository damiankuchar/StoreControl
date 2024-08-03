namespace StoreControl.Domain.Errors
{
    public class ErrorEntry
    {
        public string Message { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
        public string Property { get; set; } = string.Empty;
    }
}
