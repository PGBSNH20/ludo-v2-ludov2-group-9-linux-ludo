using System;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

namespace LinuxLudo.Web.Hubs
{
    public class HubController : IHubController, IHubConnection
    {
        private HubConnection _hub;

        public IHubConnection Connect(string hub)
        {
            var URL = Environment.GetEnvironmentVariable("API_URL") ?? "https://localhost:5001";
            _hub = new HubConnectionBuilder()
                .WithUrl($"{URL}/{hub}")
                .WithAutomaticReconnect()
                .AddMessagePackProtocol()
                .Build();

            return this;
        }

        public HubConnection Hub()
        {
            return _hub;
        }
    }
}