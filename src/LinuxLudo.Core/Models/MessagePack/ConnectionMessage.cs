using System.Collections.Generic;
using LinuxLudo.Core.Models;
using MessagePack;

namespace LinuxLudo.Core
{
    [MessagePackObject]
    public class ConnectionMessage
    {
        [Key(0)]
        public string Username { get; set; }
        [Key(1)]
        public List<Player> Players { get; set; }

        public ConnectionMessage() { }
        public ConnectionMessage(string username, List<Player> players)
        {
            Username = username;
            Players = players;
        }
    }
}