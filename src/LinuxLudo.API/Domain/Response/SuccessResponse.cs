using System;

namespace LinuxLudo.API.Domain.Response
{
    public class SuccessResponse
    {
        public string Message { get; }
        private int Code { get; }

        public SuccessResponse(string message, int status)
        {
            Message = message;
            Code = status;
        }

        public BaseResponse Respond()
        {
            return new("Success", Code, this);
        }

        public BaseResponse Respond(object data)
        {
            return new("Success", Code, data);
        }
    }
}