using LinuxLudo.Core.Models;

namespace LinuxLudo.API.Domain.Models
{
    public class ConnectedUser
    {
        public string Username { get; set; }
        public string ConnectionId { get; set; }
        public OpenGame JoinedGame { get; set; }

        public ConnectedUser(string username, string connectionId, OpenGame joinedGame)
        {
            Username = username;
            ConnectionId = connectionId;
            JoinedGame = joinedGame;
        }
    }
}