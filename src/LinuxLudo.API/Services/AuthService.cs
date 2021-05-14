using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LinuxLudo.API.Domain.Models.Auth;
using LinuxLudo.API.Domain.Response;
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

        public async Task<BaseResponse> SignInAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public async Task<BaseResponse> SingUpAsync(User user)
        {
            var existing = await _userManager.FindByNameAsync(user.UserName);

            if (existing != null)
                return new ErrorResponse("User already exists.", 409, null).Respond();

            var isCreated = await _userManager.CreateAsync(user);

            if (!isCreated.Succeeded)
                return new ErrorResponse(isCreated.Errors.Select(e => e.Description).First(), 400, null).Respond();

            return new SuccessResponse("Account created", 201, null).Respond();
        }
    }
}