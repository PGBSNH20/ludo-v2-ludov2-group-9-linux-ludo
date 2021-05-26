using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace LinuxLudo.API.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string msg)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, msg);
        }
    }
}