using System;
using MessagePack;

namespace LinuxLudo.Core
{
    [MessagePackObject]
    public class PlayerJoinMessage
    {
        [Key(0)]
        public string Username { get; set; }
        [Key(1)]
        public Guid GameId { get; set; }

        public PlayerJoinMessage() { }
        public PlayerJoinMessage(string username, Guid gameId)
        {
            Username = username;
            GameId = gameId;
        }
    }
}