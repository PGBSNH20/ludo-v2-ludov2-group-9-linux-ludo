using System;
using Microsoft.AspNetCore.Identity;

namespace LinuxLudo.API.Domain.Models.Auth
{
    public class Role : IdentityRole<Guid>
    { }
}