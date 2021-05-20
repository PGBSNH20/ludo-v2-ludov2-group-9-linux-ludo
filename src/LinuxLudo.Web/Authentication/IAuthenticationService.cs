using System.Threading.Tasks;
using LinuxLudo.Web.Models;

public interface IAuthenticationService
{
    Task<AuthenticatedUserModel> Login(AuthenticationUserModel user);
    Task Logout();
}