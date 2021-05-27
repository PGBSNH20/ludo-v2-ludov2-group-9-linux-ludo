using System;
using System.Collections.Generic;
using System.Linq;
using LinuxLudo.API.Domain.Repositories;
using LinuxLudo.Core.Models;

namespace LinuxLudo.API.Database.Repositories
{
    public class GameHubRepository : IGameHubRepository
    {
        public List<OpenGame> openGames = new();
        public void AddGame(OpenGame game)
        {
            openGames.Add(game);
        }
        public void AddPlayer(OpenGame game, string username)
        {
            string color = GetAvailableColor(game);
            Player player = new Player(color, username);
            game.PlayersInGame.Add(player);
        }

        public IEnumerable<OpenGame> FetchAllGames()
        {
            return openGames;
        }

        public OpenGame FetchGameById(Guid id)
        {
            return openGames.First(game => game.GameId == id);
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
