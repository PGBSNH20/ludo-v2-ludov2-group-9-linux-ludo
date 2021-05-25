using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace LinuxLudo.Web.Hubs
{
    public interface IHubController
    {
        public IHubConnection Connect(string hub);
    }

    public interface IHubConnection
    {
        public HubConnection Hub();
    }
}