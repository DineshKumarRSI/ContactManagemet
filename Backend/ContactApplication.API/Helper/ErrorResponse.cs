using System.Net;

namespace ContactApplication.API.Helper
{
    public class ErrorResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Title { get; set; }
        public List<string> Error { get; set; }
    }
}
