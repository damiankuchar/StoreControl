using System.Net;

namespace StoreControl.Domain.Errors
{
    public class ErrorResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<ErrorEntry> Errors { get; set; } = new List<ErrorEntry>();
    }
}
