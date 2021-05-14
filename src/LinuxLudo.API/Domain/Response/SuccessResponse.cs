using System;

namespace LinuxLudo.API.Domain.Response
{
    public class SuccessResponse
    {
        public string Message { get; }
        private int Code { get; }
        private Object Data { get; }

        public SuccessResponse(string message, int status, Object data)
        {
            Message = message;
            Code = status;
            Data = data;
        }

        public BaseResponse Respond()
        {
            return new("Success", Code, this);
        }
    }
}