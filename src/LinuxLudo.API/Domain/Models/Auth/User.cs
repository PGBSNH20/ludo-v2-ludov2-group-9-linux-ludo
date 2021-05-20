using System;
using Microsoft.AspNetCore.Identity;

namespace LinuxLudo.API.Domain.Models.Auth
{
    public class User : IdentityUser<Guid>
    {}
}