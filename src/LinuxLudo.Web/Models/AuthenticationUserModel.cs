using System.ComponentModel.DataAnnotations;

namespace LinuxLudo.Web.Models
{
    public class AuthenticationUserModel
    {
        [Required(ErrorMessage = "A username is required!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "A password is required!")]
        [MinLength(6, ErrorMessage = "Your password is too short!")]
        public string Password { get; set; }
    }
}