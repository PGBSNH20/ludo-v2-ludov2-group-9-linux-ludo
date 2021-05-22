using System.Collections.Generic;

namespace LinuxLudo.Web.Authentication
{
    public class RegisteredUserModel
    {
        public string Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}