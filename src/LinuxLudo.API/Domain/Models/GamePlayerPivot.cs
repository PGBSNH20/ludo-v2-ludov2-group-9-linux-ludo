using System;
using LinuxLudo.API.Domain.Models.Auth;

namespace LinuxLudo.API.Domain.Models
{
    public class GamePlayerPivot
    {
        // Workaround for foreign key needs id.
        public Guid Id { get; set; }
        public Guid GameId { get; set; } 
       public virtual Game Game { get; set; }
       public Guid PlayerId { get; set; }
       public virtual User Player { get; set; }
    }
}