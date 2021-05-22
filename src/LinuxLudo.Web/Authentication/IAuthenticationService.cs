using System.Threading.Tasks;
using LinuxLudo.Web.Authentication;
using LinuxLudo.Web.Models;

public interface IAuthenticationService
{
    Task<AuthenticatedUserModel> SignIn(AuthenticationUserModel user);
    Task<RegisteredUserModel> SignUp(CreateUserModel user);
    Task Logout();
}