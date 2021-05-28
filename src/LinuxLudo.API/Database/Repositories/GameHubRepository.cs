using System;
using System.Collections.Generic;
using System.Linq;
using LinuxLudo.API.Domain.Models;
using LinuxLudo.API.Domain.Repositories;
using LinuxLudo.Core.Models;

namespace LinuxLudo.API.Database.Repositories
{
    public class GameHubRepository : IGameHubRepository
    {
        public List<OpenGame> openGames = new();
        public List<ConnectedUser> connectedUsers = new();

        public void AddGame(OpenGame game) => openGames.Add(game);
        public void RemoveGame(OpenGame game) => openGames.Remove(game);

        public void AddPlayer(OpenGame game, string username)
        {
            string color = GetAvailableColor(game);
            Player player = new(color, username);
            game.PlayersInGame.Add(player);
        }

        public void ConnectUser(ConnectedUser user) => connectedUsers.Add(user);
        public void DisconnectUser(ConnectedUser user) => connectedUsers?.Remove(user);
        public ConnectedUser FetchUserById(string connectionId)
        {
            if (connectedUsers.Any(user => user.ConnectionId == connectionId))
            {
                return connectedUsers.First(user => user.ConnectionId == connectionId);
            }

            return null;
        }

        public void RemovePlayer(OpenGame game, string username) => game.PlayersInGame.Remove(game.PlayersInGame.First(player => player.Name == username));
        public IEnumerable<OpenGame> FetchAllGames() => openGames;

        public OpenGame FetchGameById(Guid id)
        {
            if (openGames.Any(game => game.GameId == id))
            {
                return openGames.First(game => game.GameId == id);
            }

            return null;
        }

        private string GetAvailableColor(OpenGame game)
        {
            List<string> availableColors = new() { "red", "green", "blue", "yellow" };
            foreach (Player player in game.PlayersInGame.ToList())
            {
                foreach (string color in availableColors.ToList())
                {
                    if (color == player.Color)
                    {
                        availableColors.Remove(color);
                    }
                }
            }

            return availableColors[0];
        }
    }
}
