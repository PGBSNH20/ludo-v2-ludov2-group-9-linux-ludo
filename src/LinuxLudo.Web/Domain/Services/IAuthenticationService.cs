using System.Threading.Tasks;
using LinuxLudo.Web.Authentication;
using LinuxLudo.Web.Domain.Models;

namespace LinuxLudo.Web.Domain.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedUserModel> SignIn(AuthenticationUserModel user);
        Task<RegisteredUserModel> SignUp(CreateUserModel user);
        Task Logout();
    }
}