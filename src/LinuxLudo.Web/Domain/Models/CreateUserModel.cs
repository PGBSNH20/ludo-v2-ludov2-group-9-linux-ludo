using System.ComponentModel.DataAnnotations;

namespace LinuxLudo.Web.Domain.Models
{
    public class CreateUserModel
    {
        [Required(ErrorMessage = "A username is required!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "A password is required!")]
        [MinLength(6, ErrorMessage = "Your password is too short!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm the password!")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match!")]
        public string ConfirmPassword { get; set; }
    }
}