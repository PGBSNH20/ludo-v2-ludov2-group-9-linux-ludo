namespace LinuxLudo.API.Domain.Response
{
    public class ErrorResponse
    {
        public string Message { get; }
        private int Code { get; }
        public string RequestId { get; }

        public ErrorResponse(string message, int status, string requestId)
        {
            Message = message;
            Code = status;
            RequestId = requestId;
        }

        public BaseResponse Respond()
        {
            return new("Error", Code, this);
        }
    }
}