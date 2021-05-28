using System.Collections.Generic;
using LinuxLudo.API.Domain.Models.Auth;

namespace LinuxLudo.API.Domain.Services
{
    public interface IJwtService
    {
        public string GenerateJwt(User user, IList<string> roles);
    }
}