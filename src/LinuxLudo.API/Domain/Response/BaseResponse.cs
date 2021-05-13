#nullable enable
using System;

namespace LinuxLudo.API.Domain.Response
{
    public class BaseResponse
    {
       public string Status { get; } 
       public int StatusCode { get; }
       public ErrorResponse Error { get; }
       public Object Data { get; }
       
       public BaseResponse(string status, int code, ErrorResponse error)
       {
           Status = status;
           StatusCode = code;
           Error = error;
       }
       public BaseResponse(string status, int code, object data)
       {
           Status = status;
           StatusCode = code;
           Data = data;
       }
    }
}