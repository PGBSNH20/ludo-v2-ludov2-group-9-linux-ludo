using System.ComponentModel.DataAnnotations;

namespace LinuxLudo.API.Domain.Resources.Auth
{
    public class SignInResource
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}