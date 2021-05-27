using LinuxLudo.Core.Models;

namespace LinuxLudo.API.Domain.Models
{
    public class ConnectedUser
    {
        public string Username { get; }
        public string ConnectionId { get; }
        public OpenGame JoinedGame { get; }

        public ConnectedUser(string username, string connectionId, OpenGame joinedGame)
        {
            Username = username;
            ConnectionId = connectionId;
            JoinedGame = joinedGame;
        }
    }
}