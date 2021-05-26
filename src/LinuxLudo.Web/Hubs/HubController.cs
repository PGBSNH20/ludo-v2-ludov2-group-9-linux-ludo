using Microsoft.AspNetCore.SignalR.Client;

namespace LinuxLudo.Web.Hubs
{
    public class HubController : IHubController, IHubConnection
    {
        private HubConnection _hub;

        public IHubConnection Connect(string hub)
        {
            _hub = new HubConnectionBuilder()
                .WithUrl($"https://localhost:5001/{hub}")
                .WithAutomaticReconnect()
                .Build();

            return this;
        }

        public HubConnection Hub()
        {
            return _hub;
        }
    }
}