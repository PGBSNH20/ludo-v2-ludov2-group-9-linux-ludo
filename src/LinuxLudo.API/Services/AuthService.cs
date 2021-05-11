using System;
using System.Threading.Tasks;
using LinuxLudo.API.Domain.Models.Auth;
using LinuxLudo.API.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinuxLudo.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;

        public AuthService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> SignInAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> SingUpAsync(User user)
        {
            var existing = await _userManager.FindByNameAsync(user.UserName);

            if (existing != null)
                return "User already exists";

            throw new NotImplementedException();
        }
    }
}