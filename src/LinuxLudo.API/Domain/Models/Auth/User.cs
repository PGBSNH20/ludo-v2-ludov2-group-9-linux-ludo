using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace LinuxLudo.API.Domain.Models.Auth
{
    public class User : IdentityUser<Guid>
    {
        public virtual PlayerStats Stats { get; set; }
        public virtual ICollection<GamePlayerPivot> Games { get; set; }
    }
}