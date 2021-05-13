using System.Threading.Tasks;
using LinuxLudo.API.Domain.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace LinuxLudo.API.Domain.Services
{
    public interface IAuthService
    {
        Task<string> SignInAsync(User user);
        Task<string> SingUpAsync(User user);
    }
}