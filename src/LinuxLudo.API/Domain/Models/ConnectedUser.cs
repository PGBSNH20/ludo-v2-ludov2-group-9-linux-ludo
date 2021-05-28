using LinuxLudo.Core.Models;
using MessagePack;

namespace LinuxLudo.API.Domain.Models
{
    [MessagePackObject]
    public class ConnectedUser
    {
        [Key(0)]
        public string Username { get; set; }
        [Key(1)]

        public string ConnectionId { get; }
        [Key(2)]

        public OpenGame JoinedGame { get; }

        public ConnectedUser() { }
        public ConnectedUser(string username, string connectionId, OpenGame joinedGame)
        {
            Username = username;
            ConnectionId = connectionId;
            JoinedGame = joinedGame;
        }
    }
}