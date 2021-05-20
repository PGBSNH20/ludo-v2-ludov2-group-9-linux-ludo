using System.ComponentModel.DataAnnotations;

namespace LinuxLudo.Web.Models
{
    public class AuthenticationUserModel
    {
        [Required(ErrorMessage = "An email address is required!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A password is required!")]
        [MinLength(6, ErrorMessage = "Your password is too short!")]
        public string Password { get; set; }
    }
}