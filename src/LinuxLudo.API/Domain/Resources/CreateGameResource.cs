using System.ComponentModel.DataAnnotations;

namespace LinuxLudo.API.Domain.Resources
{
    public class CreateGameResource
    {
        [Required]
        [MinLength(4)]
        [MaxLength(40)]
        public string Name { get; set; }
        [Required]
        [Range(2,4)]
        public int MaxPlayers { get; set; }
    }
}