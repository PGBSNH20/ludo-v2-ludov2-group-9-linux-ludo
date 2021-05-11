namespace LinuxLudo.API.Domain.Response
{
    public class ErrorResponse
    {
        public string Error { get; set; }
        public int StatusCode { get; set; }
        public string RequestId { get; set; }
    }
}