using System.Threading.Tasks;
using LinuxLudo.API.Domain.Models.Auth;
using LinuxLudo.API.Domain.Response;
using Microsoft.AspNetCore.Mvc;

namespace LinuxLudo.API.Domain.Services
{
    public interface IAuthService
    {
        Task<BaseResponse> SignInAsync(User user, string password);
        Task<BaseResponse> SingUpAsync(User user, string password);
    }
}