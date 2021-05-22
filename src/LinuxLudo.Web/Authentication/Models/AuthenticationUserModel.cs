using System.ComponentModel.DataAnnotations;

namespace LinuxLudo.Web.Models
{
    public class AuthenticationUserModel
    {
        [Required(ErrorMessage = "A username is required!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "A password is required!")]
        public string Password { get; set; }

        public AuthenticationUserModel() { }
        public AuthenticationUserModel(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}